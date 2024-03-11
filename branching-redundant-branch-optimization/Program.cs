
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

        [Benchmark]
        public void BranchPrediction(){
            int[] myArr ;
            myArr = new int[] {1,2,3,4,5};

            int ind = s_rand.Next(1, 10)
            if(ind < 5){
                myArr [ind] = 4;
            }

    }

}