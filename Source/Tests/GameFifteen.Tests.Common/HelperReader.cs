// <copyright file="HelperWriter.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Tests.Common
{
    using System;
    using System.IO;

    public class HelperReader : TextReader
    {
        public HelperReader()
        {
        }

        public override string ReadLine()
        {
            return string.Empty;
        }
    }
}
