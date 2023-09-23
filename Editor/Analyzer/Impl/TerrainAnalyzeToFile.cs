using System.Collections.Generic;
using UTJ.ProfilerReader.BinaryData;

namespace UTJ.ProfilerReader.Analyzer
{
    public class TerrainAnalyzeToFile : CustomAnalyzeToTextbaseFileBase 
    {
       struct TerrainEvent
       {
           public int numTerrainAreas;
       }

        private List<TerrainEvent> terrainEvents = new List<TerrainEvent>(2048);
        

        public override void CollectData(ProfilerFrameData frameData)
        {
            if( frameData == null)
            {
                return;
            }


            if (frameData.frameIndex > 30)
            {
                UnityEditor.EditorApplication.Exit(0);
            }
            
            System.Console.WriteLine("RTT is " + frameData.allStats.networkMessageStats.rtt);
            

            int numTerrainAreas = 0;
            string name = "Number of Terrain Areas (Client)";
            if (frameData.TryGetCounterAsType(name, out int terrainAreas))
            {
                numTerrainAreas = terrainAreas;
            }
            
            
            terrainEvents.Add(new TerrainEvent{numTerrainAreas = numTerrainAreas});
        }
        
        protected override string GetResultText()
        {
            CsvStringGenerator csvStringGenerator = new CsvStringGenerator();
            AppendHeaderToStringBuilder(csvStringGenerator);

            foreach (var evt in terrainEvents)
            {
                csvStringGenerator.AppendColumn(evt.numTerrainAreas);
                csvStringGenerator.NextRow();
            }

            return csvStringGenerator.ToString();
        }
        
        private void AppendHeaderToStringBuilder(CsvStringGenerator csvStringGenerator)
        {
            csvStringGenerator.AppendColumn("numTerrainAreas");
            csvStringGenerator.NextRow();
        }
        
        protected override string FooterName
        {
            get
            {
                return "_terrain.csv";
            }
        }
    }
}