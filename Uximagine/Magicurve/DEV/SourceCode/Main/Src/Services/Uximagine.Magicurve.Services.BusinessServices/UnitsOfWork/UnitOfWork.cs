using System;
using System.Globalization;
using System.Transactions;
using Uximagine.Magicurve.Core.Diagnostics.ExceptionManagement;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    using Uximagine.Magicurve.Core;
    using Uximagine.Magicurve.Core.Diagnostics;
    using Uximagine.Magicurve.Core.Diagnostics.Logging;
    using Uximagine.Magicurve.DataTransfer.Exceptions;

    /// <summary>
    /// Defines the functionality of a Unit of Work.
    /// </summary>
    public abstract class UnitOfWork
    {
        #region Classes

        /// <summary>
        /// Contains exception policies for the Unit of Work.
        /// </summary>
        private static class ExceptionPolicy
        {
            #region Constants
            /// <summary>
            /// Exception policy for errors occurring during the actual work.
            /// </summary>
            public const string DoWork = @"UnitOfWork_Do";

            /// <summary>
            /// Exception policy for errors occurring during cleanup.
            /// </summary>
            public const string AttemptCleanUp = @"UnitOfWork_Cleanup";

            #endregion
        }

        /// <summary>
        /// Contains operations log messages.
        /// </summary>
        private static class OperationsLogMessages
        {
            #region Constants
            /// <summary>
            /// Logs the start of operations.
            /// </summary>
            public const string LogStart = @"[{0}] - START";

            /// <summary>
            /// Logs the end of operations.
            /// </summary>
            public const string LogEnd = @"[{0}] - END";

            /// <summary>
            /// Logs the start of the pre-execute phase.
            /// </summary>
            public const string LogStartPreExecute = @"[{0}] - Pre-Execute/Start";

            /// <summary>
            /// Logs the end of the pre-execute phase.
            /// </summary>
            public const string LogEndPreExecute = @"[{0}] - Pre-Execute/End";

            /// <summary>
            /// Logs the start of the actual Work.
            /// </summary>
            public const string LogStartWork = @"[{0}] - Work/Start";

            /// <summary>
            /// Logs the end of the actual Work.
            /// </summary>
            public const string LogEndWork = @"[{0}] - Work/End";

            /// <summary>
            /// Logs the start of the post-execute phase.
            /// </summary>
            public const string LogStartPostExecute = @"[{0}] - Post-Execute/Start";

            /// <summary>
            /// Logs the end of the post-execute phase.
            /// </summary>
            public const string LogEndPostExecute = @"[{0}] - Post-Execute/End";

            /// <summary>
            /// Logs the start of error handling.
            /// </summary>
            public const string LogStartErrorHandling = @"[{0}] - Error-Handling/Start";

            /// <summary>
            /// Logs the end of error handling.
            /// </summary>
            public const string LogEndErrorHandling = @"[{0}] - Error-Handling/End";

            /// <summary>
            /// Logs the start of cleanup operations.
            /// </summary>
            public const string LogStartCleanup = @"[{0}] - Cleanup/Start";

            /// <summary>
            /// Logs the end of cleanup operations.
            /// </summary>
            public const string LogEndCleanup = @"[{0}] - Cleanup/End";

            #endregion
        }

        #endregion

        #region Properties - Instance Member

        #region Properties - Instance Member - UnitOfWork Members

        /// <summary>
        /// Gets a value indicating whether this Unit of Work is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this Unit of Work is read only; 
        /// otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the operations log.
        /// </summary>
        /// <value>
        /// The operations log.
        /// </value>
        private OperationsCollection OperationsLog
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Methods - Instance Member

        #region Methods - Instance Member - UnitOfWork Members

        #region Methods - Instance Member - UnitOfWork Members - (constructors)

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly">
        /// <c>true</c> if this Unit of Work is read only; 
        /// otherwise, <c>false</c>.
        /// </param>
        protected UnitOfWork(bool isReadOnly)
        {
            this.IsReadOnly = isReadOnly;
            this.OperationsLog = new OperationsCollection();
        }

        #endregion

        #region Methods - Instance Member - UnitOfWork Members - (operations)

        /// <summary>
        /// Does the Work.
        /// </summary>
        /// <exception cref="BusinessException">
        /// An error occurs while doing the Work.
        /// </exception>
        public void DoWork()
        {
            this.LogStart();

            bool hasErrorOccurred = false;

            try
            {
                this.AttemptToDoWork();
            }
            catch (Exception ex)
            {
                this.LogStartErrorHandling();

                hasErrorOccurred = true;

                Exception handledException = ExceptionManager.HandleException(
                    this.GetType(),
                    ex,
                    ErrorSeverity.Error,
                    UnitOfWork.ExceptionPolicy.DoWork);

                if (handledException != null)
                {
                    this.LogEndErrorHandling();
                    throw new BusinessException(handledException.Message, handledException);
                }
                else
                {
                    this.LogEndErrorHandling();
                    throw new BusinessException(ex.Message, ex);
                }
            }
            finally
            {
                this.LogStartCleanup();
                this.AttemptCleanUp(hasErrorOccurred);
                this.LogEndCleanup();

                this.LogEnd();

                this.WriteOperationsLog();
            }
        }

        /// <summary>
        /// Attempts to do the Work.
        /// </summary>
        private void AttemptToDoWork()
        {
            this.LogStartPreExecute();
            this.PreExecute();
            this.LogEndPreExecute();

            if (this.IsReadOnly)
            {
                if (ConfigurationData.MustSerializeReadOperations)
                {
                    this.ExecuteTransaction();
                }
                else
                {
                    this.ExecuteNonTransaction();
                }
            }
            else
            {
                this.ExecuteTransaction();
            }

            this.LogStartPostExecute();
            this.PostExecute();
            this.LogEndPostExecute();
        }

        /// <summary>
        /// Executes the Unit of Work in a transactional manner.
        /// </summary>
        private void ExecuteTransaction()
        {
            this.LogStartWork();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                this.Execute();

                transactionScope.Complete();
            }

            this.LogEndWork();
        }

        /// <summary>
        /// Executes the Unit of Work in a non-transactional manner.
        /// </summary>
        private void ExecuteNonTransaction()
        {
            this.LogStartWork();

            this.Execute();

            this.LogEndWork();
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected virtual void PreExecute()
        {
        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected virtual void PostExecute()
        {
        }

        /// <summary>
        /// Attempts to clean up the Unit of Work.
        /// </summary>
        /// <param name="hasErrorOccurred">
        /// <c>true</c> if an error has occurred;
        /// otherwise, <c>false</c>.
        /// </param>
        private void AttemptCleanUp(bool hasErrorOccurred)
        {
            try
            {
                this.CleanUp(hasErrorOccurred);
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(
                    this.GetType(),
                    ex,
                    ErrorSeverity.Warning,
                    UnitOfWork.ExceptionPolicy.AttemptCleanUp);

                //// sink exception
            }
        }

        /// <summary>
        /// Cleans up the Unit of Work.
        /// </summary>
        /// <param name="hasErrorOccurred">
        /// <c>true</c> if an error has occurred;
        /// otherwise, <c>false</c>.
        /// </param>
        /// <remarks>
        /// Errors in this method will not be allowed to trickle up the call stack,
        /// to allow the original exception data to be made available.
        /// </remarks>
        protected virtual void CleanUp(bool hasErrorOccurred)
        {
        }

        #endregion

        #region Methods - Instance Member - UnitOfWork Members - (diagnostics)

        /// <summary>
        /// Logs the given operations message.
        /// </summary>
        /// <param name="message">
        /// The message describing the operation.
        /// </param>
        protected void LogOperation(string message)
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                Operation operation = new Operation();

                operation.Timestamp = DateTimeHelper.Now;
                operation.Message = string.Format(
                    CultureInfo.InvariantCulture,
                    message,
                    this.GetType().Name);

                this.OperationsLog.Add(operation);
            }
        }

        /// <summary>
        /// Writes the operations log.
        /// </summary>
        protected void WriteOperationsLog()
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                string logText = this.OperationsLog.ToString();

                LogManager.Log(
                    this.GetType(),
                    ErrorSeverity.Information,
                    logText);
            }
        }

        /// <summary>
        /// Logs the start of operations.
        /// </summary>
        private void LogStart()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStart);
        }

        /// <summary>
        /// Logs the end of operations.
        /// </summary>
        private void LogEnd()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEnd);
        }

        /// <summary>
        /// Logs the start of the pre-execute phase.
        /// </summary>
        private void LogStartPreExecute()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStartPreExecute);
        }

        /// <summary>
        /// Logs the end of the pre-execute phase.
        /// </summary>
        private void LogEndPreExecute()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEndPreExecute);
        }

        /// <summary>
        /// Logs the start of the actual Work.
        /// </summary>
        private void LogStartWork()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStartWork);
        }

        /// <summary>
        /// Logs the end of the actual Work.
        /// </summary>
        private void LogEndWork()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEndWork);
        }

        /// <summary>
        /// Logs the start of the post-execute phase.
        /// </summary>
        private void LogStartPostExecute()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStartPostExecute);
        }

        /// <summary>
        /// Logs the end of the post-execute phase.
        /// </summary>
        private void LogEndPostExecute()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEndPostExecute);
        }

        /// <summary>
        /// Logs the start of error handling.
        /// </summary>
        private void LogStartErrorHandling()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStartErrorHandling);
        }

        /// <summary>
        /// Logs the end of error handling.
        /// </summary>
        private void LogEndErrorHandling()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEndErrorHandling);
        }

        /// <summary>
        /// Logs the start of cleanup operations.
        /// </summary>
        private void LogStartCleanup()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogStartCleanup);
        }

        /// <summary>
        /// Logs the end of cleanup operations.
        /// </summary>
        private void LogEndCleanup()
        {
            this.LogOperation(UnitOfWork.OperationsLogMessages.LogEndCleanup);
        }

        #endregion

        #endregion

        #endregion
    }
}
