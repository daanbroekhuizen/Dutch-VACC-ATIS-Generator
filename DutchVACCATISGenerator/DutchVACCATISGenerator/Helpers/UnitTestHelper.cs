using System;
using System.Linq;

namespace DutchVACCATISGenerator.Helpers
{
    public class UnitTestHelper
    {
        /// <summary> 
        /// Detects if the current code is being executed from a unit test. 
        /// </summary> 
        /// <returns></returns> 
        public static bool Detect_IsUnitTestRunning()
        {
            string testAssemblyName = "Microsoft.VisualStudio.QualityTools.UnitTestFramework";

            return AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.StartsWith(testAssemblyName));
        }
    }
}
