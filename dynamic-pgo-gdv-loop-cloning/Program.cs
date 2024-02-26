
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

BenchmarkRunner.Run<Benchmarks.Benchmark>();

namespace Benchmarks{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [HideColumns("Error", "StdDev", "Allocated")]
    public class Benchmark{
        private Func<int> _func;

        private int getNumber(){
            return 2;
        }

        [GlobalSetup]
        public void Setup() => _func = getNumber;
    
        [Benchmark]
        public int Sum(){
            int sum =0;
            for(int i=0;i< 100_000;i++){
                sum += _func();
            }

            return sum;
        }
    }

}