
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

BenchmarkRunner.Run<Benchmarks.Benchmark>();

namespace Benchmarks{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [HideColumns("Error", "StdDev",  "Median", "RatioSD")]
    [DisassemblyDiagnoser]
    public class Benchmark{
        private static readonly Random s_rand = new();
        /*        
        class Dummy{
            public static int None;
            public static int GetRandom1(){
                return  s_rand.Next(0, 101);
            }
        }
        

        [Benchmark]
        public int CondInstEmissionTest()=> CondInstEmission( s_rand.NextDouble() < Probability);
        public int CondInstEmission(bool val)=> val ? Dummy.GetRandom1(): Dummy.None;
        */

        [Benchmark]
        public int GetMax(){
            int n1 = s_rand.Next(0, 101);
            int n2 = s_rand.Next(0, 101);
            int ans = n1>n2 ? n1: n2;
            return ans;
        }
    }
}