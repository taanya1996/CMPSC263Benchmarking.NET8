# HashTable vs Dictionary vs HybridDictionary

The benchmark aims to compare the performance of hashtable vs dictionary. A dictionary is internally implemented as a hashtable in .NET and provides type safety while HashTable does not. This means objects added/ retrieved to/ from a dictionary should be of specified types. Dictionary is also a generic collectection while HashTable is non-generic. HybridDictionary is a performant datastructure similar to dictionary/ HashTable which uses a ListDictionary when small (< 10 items) and HashTable when large

## Benchmark


## Results


## Observations


## How to run the benchmark
```shell
dotnet run -c Release
```
