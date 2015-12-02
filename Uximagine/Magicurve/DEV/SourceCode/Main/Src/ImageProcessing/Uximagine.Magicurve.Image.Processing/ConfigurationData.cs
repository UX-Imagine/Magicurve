﻿using System.Collections.Specialized;
using System.Configuration;

namespace Uximagine.Magicurve.Image.Processing
{
    /// <summary>
    /// Provides access to configuration data.
    /// </summary>
    internal static class ConfigurationData
    {
        #region Classes

        /// <summary>
        /// Contains plug in names.
        /// </summary>
        private static class PluginNames
        {
            #region Constants

            /// <summary>
            /// Represents the name of the service implementation plug in.
            /// </summary>
            public const string MatcherImplementation = @"MatcherImpl";

            /// <summary>
            /// The detector implementation.
            /// </summary>
            public const string EdgeDetectorImplementation = @"EdgeDetectorImpl";

            /// <summary>
            /// The BLOB detector implementation.
            /// </summary>
            public const string BlobDetectorImplementation = @"BlobDetectorImpl";

            /// <summary>
            /// The shape checker implementation plugin name.
            /// </summary>
            public const string ShapeCheckerImplementationPluginName = @"ShapeCheckerImpl";

            #endregion
        }

        private static class DirectoryNames
        {
            /// <summary>
            /// The train data section name
            /// </summary>
            public const string TrainDataSectionName = @"TrainData";

        }

        #endregion

        /// <summary>
        /// Gets the name of the service implementation plug-in.
        /// </summary>
        /// <value>
        /// The name of the service implementation plug-in.
        /// </value>
        public static string MatcherImplementationPluginName
        {
            get
            {
                return ConfigurationData.PluginNames.MatcherImplementation;
            }
        }

        /// <summary>
        /// Gets the name of the detector implementation plug-in.
        /// </summary>
        /// <value>
        /// The name of the detector implementation plug-in.
        /// </value>
        public static string EdgeDetectorImplementationPluginName
        {
            get
            {
                return ConfigurationData.PluginNames.EdgeDetectorImplementation;
            }
        }

        /// <summary>
        /// Gets the name of the BLOB detector implementation plug-in.
        /// </summary>
        /// <value>
        /// The name of the BLOB detector implementation plug-in.
        /// </value>
        public static string BlobDetectorImplementationPluginName
        {
            get
            {
                return ConfigurationData.PluginNames.BlobDetectorImplementation;
            }
        }

        /// <summary>
        /// Gets the name of the shape checker implementation plugin.
        /// </summary>
        /// <value>
        /// The name of the shape checker implementation plugin.
        /// </value>
        public static string ShapeCheckerImplementationPluginName
        {
            get
            {
                return ConfigurationData.PluginNames.ShapeCheckerImplementationPluginName;
            }
        }

        /// <summary>
        /// Gets the train data directory.
        /// </summary>
        /// <value>
        /// The train data directory.
        /// </value>
        public static NameValueCollection TrainDataInfo
        {
            get
            {
                NameValueCollection trainData =
                    (NameValueCollection)ConfigurationManager.GetSection(DirectoryNames.TrainDataSectionName);

                return trainData;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [must log operational performance].
        /// </summary>
        /// <value>
        /// <c>true</c> if [must log operational performance]; otherwise, <c>false</c>.
        /// </value>
        public static bool MustLogOperationalPerformance { get { return true; } }

        /// <summary>
        /// Gets the minimum size.
        /// </summary>
        /// <value>
        /// The minimum size.
        /// </value>
        public static int MinSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["minSize"]);
            }
        }
    }
}
