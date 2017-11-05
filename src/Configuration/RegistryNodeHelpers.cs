using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Microsoft.Win32;

using org.Core.Reflection;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegistryNodeHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static RegistryNodeHelpers()
        {
        }
        #endregion

        #region Fields
        
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IRegistryPathInfo ExtractRegistryPathProvider(object instance)
        {
            try
            {
                return AttributeMemberHelpers
                    .RetrieveMember(instance, typeof(IRegistryPathInfo), false) as IRegistryPathInfo;
            }
            catch(NullReferenceException)
            {
                if (instance is IRegistryPathInfo)
                    return (instance as IRegistryPathInfo);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathProvider"></param>
        /// <returns></returns>
        public static RegistryKey GetRegistryKey(IRegistryPathInfo pathProvider)
        {
            using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(pathProvider.RootType))
            {
                return _registryKey.CreateSubKey(pathProvider.NodeName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootType"></param>
        /// <returns></returns>
        public static RegistryKey GetRegistryKey(RegistryRootType rootType)
        {
            switch (rootType)
            {
                case RegistryRootType.CLASSES_ROOT:
                    return Registry.ClassesRoot;

                case RegistryRootType.CURRENT_USER:
                    return Registry.CurrentUser;

                case RegistryRootType.LOCAL_MACHINE:
                    return Registry.LocalMachine;

                case RegistryRootType.USERS:
                    return Registry.Users;

                case RegistryRootType.CURRENT_CONFIG:
                    return Registry.CurrentConfig;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootType"></param>
        /// <returns></returns>
        public static string GetRegistryNodeName(RegistryRootType rootType)
        {
            switch (rootType)
            {
                case RegistryRootType.CLASSES_ROOT:
                    return "HKEY_CLASSES_ROOT";

                case RegistryRootType.CURRENT_USER:
                    return "HKEY_CURRENT_USER";

                case RegistryRootType.LOCAL_MACHINE:
                    return "HKEY_LOCAL_MACHINE";

                case RegistryRootType.USERS:
                    return "HKEY_USERS";

                case RegistryRootType.CURRENT_CONFIG:
                    return "HKEY_CURRENT_CONFIG";
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathProvider"></param>
        /// <returns></returns>
        public static string GetRegistryNodeName(IRegistryPathInfo pathProvider)
        {
            return string.Format(@"{0}\{1}", GetRegistryNodeName(pathProvider.RootType), pathProvider.NodeName);
        }
        #endregion
    }
}
