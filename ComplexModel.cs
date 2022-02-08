using System.Collections.Generic;

namespace deserialization_benchmark
{
    public class ComplexModel
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public bool Property4 { get; set; }
        public string Property5 {get; set; }
        public char Property6 {get; set; }
        public List<ComplexModel> NestedModel { get; set; }
    }
}