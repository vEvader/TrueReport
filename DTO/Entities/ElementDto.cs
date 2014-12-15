using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class ElementDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public ElementType Type { get; set; }
        public string BindValue { get; set; }
        public int Id { get; set; }
    }
}
