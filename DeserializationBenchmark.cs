using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using newtonsoft = Newtonsoft.Json;
using textjson = System.Text.Json;

namespace deserialization_benchmark
{
    [HtmlExporter]
    public class DeserializationBenchmark
    {
        private string _serializedSimpleModelList { get; set; }
        private string _serializedComplexModelList { get; set; }

        public DeserializationBenchmark()
        {
            _serializedSimpleModelList = SerializeSimpleModelList();
            _serializedComplexModelList = SerializeComplexModelList();
        }

        [Benchmark]
        public List<SimpleModel> JTextSimple()
        {
            return textjson.JsonSerializer.Deserialize<List<SimpleModel>>(_serializedSimpleModelList);
        }

        [Benchmark]
        public List<SimpleModel> NewtonSimple()
        {
            return newtonsoft.JsonConvert.DeserializeObject<List<SimpleModel>>(_serializedSimpleModelList);
        }

        [Benchmark]
        public List<ComplexModel> JTextComplex()
        {
            return textjson.JsonSerializer.Deserialize<List<ComplexModel>>(_serializedComplexModelList);
        }

        [Benchmark]
        public List<ComplexModel> NewtonComplex()
        {
            return newtonsoft.JsonConvert.DeserializeObject<List<ComplexModel>>(_serializedComplexModelList);
        }

        private string SerializeSimpleModelList()
        {
            var random = new Random();
            var simpleModelList = new List<SimpleModel>();
            
            for(var i = 0; i < 5; i++)
            {
                simpleModelList.Add(new SimpleModel { PointX = random.NextDouble(), PointY = random.NextDouble() });
            }

            return textjson.JsonSerializer.Serialize(simpleModelList);
        }

        private string SerializeComplexModelList()
        {
            var random = new Random();
            var complexModelList = new List<ComplexModel>();
            
            var baseComplexModel = new ComplexModel
            {
                Property1 = this.GetHashCode().ToString(),
                Property2 = random.Next().ToString(),
                Property3 = random.NextDouble().ToString(),
                Property4 = true,
                Property5 = float.MaxValue.ToString(),
                Property6 = 'b'
                
            };

            for(var i = 0; i < 5; i++)
            {
                var complexModel = new ComplexModel
                {
                    Property1 = this.GetHashCode().ToString(),
                    Property2 = random.Next().ToString(),
                    Property3 = random.NextDouble().ToString(),
                    Property4 = true,
                    Property5 = float.MaxValue.ToString(),
                    Property6 = 'a',
                    NestedModel = new List<ComplexModel>()
                };

                for(var j = 0; j < 2; j++)
                {
                    complexModel.NestedModel.Add(baseComplexModel);
                }

                complexModelList.Add(complexModel);
            }
            
            return textjson.JsonSerializer.Serialize(complexModelList);
        }
    }
}