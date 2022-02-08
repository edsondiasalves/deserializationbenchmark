using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Running;

namespace deserialization_benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.CreateMinimumViable();
            config.AddExporter(CsvMeasurementsExporter.Default);
            config.AddExporter(RPlotExporter.Default);
            
            BenchmarkRunner.Run<DeserializationBenchmark>(config);
        }
    }
}
