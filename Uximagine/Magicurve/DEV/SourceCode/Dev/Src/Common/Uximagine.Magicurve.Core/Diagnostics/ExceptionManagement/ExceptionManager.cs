using System;
using System.Globalization;
using Uximagine.Magicurve.Core.Diagnostics.Logging;
using Uximagine.Magicurve.Core.Reflection;

namespace Uximagine.Magicurve.Core.Diagnostics.ExceptionManagement
{
    /// <summary>
    /// The exception manager.
    /// </summary>
    public static class ExceptionManager
    {
        #region Constants

        /// <summary>
        /// The default exception policy name.
        /// </summary>
        private const string DefaultPolicyName = @"DefaultExceptionPolicy";

        #endregion

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// The exception to be thrown by the caller (as specified by <paramref name="policy"/>);
        /// otherwise, <c>null</c>.
        /// </returns>
        public static Exception HandleException(Type type, Exception exception, ErrorSeverity severity, string policy)
        {
            Exception returnedException = null;

            //// check whether the provided exception is valid.
            if (exception != null)
            {
                try
                {
                    //// validate exception policy.
                    string validationPolicy = string.IsNullOrEmpty(policy) ? ExceptionManager.DefaultPolicyName : policy;

                    //// prepare exception message.
                    string message = string.Format(
                        CultureInfo.CurrentCulture,
                        Resource.ExceptionMessageText,
                        validationPolicy,
                        exception.Message);

                    //// log exception data.
                    LogManager.Log(type, severity, message, exception);

                    returnedException = ExceptionManager.GetException(exception, validationPolicy);

                }
                catch (Exception ex)
                {
                    LogManager.Log(typeof(ExceptionManager), ErrorSeverity.Error, ex.Message);

                    returnedException = ex;
                }
            }

            return returnedException;
        }

        /// <summary>
        /// Handles the exception with throw.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="policy">The policy.</param>
        public static void HandleExceptionWithThrow(
                    Type type,
                    Exception exception,
                    ErrorSeverity severity,
                    string policy)
        {
            //// handle the exception
            Exception returnedException = ExceptionManager.HandleException(
                type,
                exception,
                severity,
                policy);

            //// identify if the returned exception is a valid reference
            if (returnedException != null)
            {
                //// throw the exception
                throw returnedException;
            }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="validationPolicy">The validation policy.</param>
        /// <returns>
        /// The exception with type.
        /// </returns>
        private static Exception GetException(Exception exception, string validationPolicy)
        {
            HandlingApproach approach = HandlingApproach.Rethrow;
            Type type = null;

            type = exception.GetType();

            return ExceptionManager.ApplyExceptionManagementPolicySettings(exception, approach, type);
        }

        /// <summary>
        /// Applies the exception management policy settings.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="approach">The approach.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The policy exception.
        /// </returns>
        private static Exception ApplyExceptionManagementPolicySettings(Exception exception, HandlingApproach approach, Type type)
        {
            Exception returnedException = null;

            switch (approach)
            {
                case HandlingApproach.Rethrow:
                    returnedException = exception;
                    break;

                case HandlingApproach.Sink:
                    returnedException = null;
                    break;

                case HandlingApproach.Wrap:
                    try
                    {
                        returnedException = (Exception)ObjectFactory.CreateObject(type, exception.Message, exception);
                    }
                    catch (Exception)
                    {
                        returnedException = exception;
                    }
                    break;

                case HandlingApproach.Swap:
                    try
                    {
                        returnedException = (Exception)ObjectFactory.CreateObject(type, exception.Message);
                    }
                    catch (Exception)
                    {
                        returnedException = exception;
                    }

                    break;

                default:
                    returnedException = exception;
                    break;
            }

            return returnedException;
        }
    }
}
