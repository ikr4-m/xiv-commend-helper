using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace CommendMe.Extension
{
    public static class ReflectionExtension
    {
        public static IEnumerable<Type> GetAssociatedNamespace(this Assembly assembly, string namespaceName)
        {
            return assembly.GetTypes().Where(x => x.IsClass && x.Namespace == namespaceName);
        }

        public static IEnumerable<Type> GetAssociatedNamespace<T>(this Assembly assembly, string namespaceName)
        {
            return assembly.GetTypes().Where(x => x.IsClass && x.Namespace == namespaceName)
                .Where(x => x.IsSubclassOf(typeof(T)));
        }
    }
}