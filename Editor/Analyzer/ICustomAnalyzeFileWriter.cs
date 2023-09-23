using System.Collections;
using System.Collections.Generic;
using UTJ.ProfilerReader.BinaryData;

namespace UTJ.ProfilerReader.Analyzer
{

    public interface ICustomAnalyzeFileWriter : IAnalyzeFileWriter
    {

        bool IsCustom()
        {
            return true;
        }

    }
}