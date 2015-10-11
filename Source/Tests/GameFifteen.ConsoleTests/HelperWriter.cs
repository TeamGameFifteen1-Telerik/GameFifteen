// <copyright file="HelperWriter.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.ConsoleTests
{
    using System;
    using System.IO;
    using System.Linq;

    public class HelperWriter : TextWriter
    {
        public HelperWriter()
        {
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Unicode; }
        }

        public override void Write(string value)
        {
            //// do mocking ...
        }
    }
}
