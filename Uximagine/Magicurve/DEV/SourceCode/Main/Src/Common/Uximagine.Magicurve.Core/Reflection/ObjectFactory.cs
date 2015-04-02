using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace Uximagine.Magicurve.Core.Reflection
{
    /// <summary>
    /// Represents a factory for type instances.
    /// </summary>
    public static class ObjectFactory
    {

        #region Methods - Static Member

        #region Methods - Static Member - (constructors)

        /// <summary>
        /// Initializes static members of the <see cref="ObjectFactory"/> class.
        /// </summary>
        static ObjectFactory()
        {
            StructureMap.ObjectFactory.Initialize(action =>
            {
                action.PullConfigurationFromAppConfig = true;
            });

            string whatIHave = StructureMap.ObjectFactory.WhatDoIHave();

#if DEBUG
            Debug.WriteLine(whatIHave);
#else
			PremiaSoft.Spindle.Core.Diagnostics.Logging.LogManager.Log(
				typeof(ObjectFactory),
				PremiaSoft.Spindle.Core.Diagnostics.ErrorSeverity.Information,
				whatIHave);
#endif
        }

        #endregion

        #region Methods - Static Member - (basic factories)

        /// <summary>
        /// Creates an instance of the type specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="type">
        /// The preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(Type type)
        {
            return ObjectFactory.CreateObject(type, null);
        }

        /// <summary>
        /// Creates an instance of the type specified, using the 
        /// most suitable constructor.
        /// </summary>
        /// <param name="type">
        /// The preferred type.
        /// </param>
        /// <param name="args">
        /// The arguments to be passed to the constructor.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(Type type, params object[] args)
        {
            object returnObject = null;

            try
            {
                // check whether constructor arguments have not been provided
                if (args == null)
                {
                    // attempt to create the instance using the default constructor
                    returnObject = Activator.CreateInstance(type);
                }
                else
                {
                    // attempt to create the instance using the most suitable constructor
                    returnObject = Activator.CreateInstance(type, args);
                }
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string typeName)
        {
            object[] args = null;

            return ObjectFactory.CreateObject(typeName, args);
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the 
        /// most suitable constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <param name="args">
        /// The arguments to be passed to the constructor.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string typeName, params object[] args)
        {
            object returnObject = null;

            try
            {
                Type type = Type.GetType(typeName);

                // check whether constructor arguments have not been provided
                if (args == null)
                {
                    // attempt to create the instance using the default constructor
                    returnObject = Activator.CreateInstance(type);
                }
                else
                {
                    // attempt to create the instance using the most suitable constructor
                    returnObject = Activator.CreateInstance(type, args);
                }
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the 
        /// named assembly and default constructor.
        /// </summary>
        /// <param name="assemblyName">
        /// The name of the assembly where the type named <paramref name="typeName"/> is sought. 
        /// If <paramref name="assemblyName"/> is <c>null</c>, the executing assembly is searched.
        /// </param>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string assemblyName, string typeName)
        {
            object returnObject = null;

            try
            {
                System.Runtime.Remoting.ObjectHandle handle =
                    Activator.CreateInstance(assemblyName, typeName);

                returnObject = handle.Unwrap();
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        #endregion

        #region Methods - Static Member - (IoC factories)

        /// <summary>
        /// Gets an instance of the type whose name is specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object GetInstance(string typeName)
        {
            Type type = Type.GetType(typeName);

            return ObjectFactory.GetInstance(type);
        }

        /// <summary>
        /// Gets an instance of the type specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="pluginType">
        /// The preferred plugin type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object GetInstance(Type pluginType)
        {
            object returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetInstance(pluginType);
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Gets an instance of the generic type specified, using the 
        /// default constructor.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        /// <typeparam name="T">
        /// The type of the instance to be plugged in.
        /// </typeparam>
        public static T GetInstance<T>()
        {
            T returnObject = default(T);

            try
            {
                returnObject = StructureMap.ObjectFactory.GetInstance<T>();
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Gets the named instance of the type whose name is specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <param name="pluginName">
        /// The identifier of the plugin.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object GetInstance(string typeName, string pluginName)
        {
            Type type = Type.GetType(typeName);

            return ObjectFactory.GetInstance(type, pluginName);
        }

        /// <summary>
        /// Gets the named instance of the type specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="pluginType">
        /// The preferred plugin type.
        /// </param>
        /// <param name="pluginName">
        /// The identifier of the plugin.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object GetInstance(Type pluginType, string pluginName)
        {
            object returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetNamedInstance(pluginType, pluginName);
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Gets the named instance of the generic type specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="pluginName">
        /// The identifier of the plugin.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        /// <typeparam name="T">
        /// The type of the instance to be plugged in.
        /// </typeparam>
        public static T GetInstance<T>(string pluginName)
        {
            T returnObject = default(T);

            try
            {
                returnObject = StructureMap.ObjectFactory.GetNamedInstance<T>(pluginName);
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Gets the instances of the type whose name is specified, using the 
        /// default constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred plugin type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static IList GetAllInstances(string typeName)
        {
            Type type = Type.GetType(typeName);

            return ObjectFactory.GetAllInstances(type);
        }

        /// <summary>
        /// Gets all instances of the generic type specified, using the 
        /// default constructor of each instance.
        /// </summary>
        /// <param name="pluginType">
        /// The preferred plugin type.
        /// </param>
        /// <returns>
        /// A list containing the created instances.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static IList GetAllInstances(Type pluginType)
        {
            IList returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetAllInstances(pluginType);
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        /// <summary>
        /// Gets all instances of the generic type specified, using the 
        /// default constructor of each instance.
        /// </summary>
        /// <returns>
        /// A list containing the created instances.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        /// <typeparam name="T">
        /// The type of the instance to be plugged in.
        /// </typeparam>
        public static IList<T> GetAllInstances<T>()
        {
            IList<T> returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetAllInstances<T>();
            }
            catch (Exception ex)
            {
                throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }

        #endregion

        #endregion

    }
}
