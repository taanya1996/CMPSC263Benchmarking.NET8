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
    public class Benchmark{
        
        internal interface ICalculator{
            int Calculate(int a, int b);
        }

        public class Calculator_Imp : ICalculator{
            public int Calculate(int a, int b) => a + b;
        }

        private ICalculator _calculator;
        private int a =2;
        private int b=3;

        [GlobalSetup]
        public void Setup() => _calculator = new Calculator_Imp();

        [Benchmark]
        public int Calculate() => _calculator.Calculate(a,b);
    }

}