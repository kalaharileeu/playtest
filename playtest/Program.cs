using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;

namespace playtest
{
    public class map
    {
        [XmlAttribute]
        public int width;
        [XmlAttribute]
        public int height;
        [XmlAttribute]
        public int tilewidth;
        [XmlAttribute]
        public int tileheight;
        [XmlElement("tileset")]
        public tileset tilesetinfo;
        [XmlElement("tilelayer")]
        public List<tilelayer> Layer;


        public map()
        {
            //tilelayer = new tilelayer();
            Layer = new List<tilelayer>();
            //TileDimensions = Vector2.Zero;
        }
    }

    public class tileset
    {
        [XmlAttribute]
        public int tilewidth;
        [XmlAttribute]
        public int tileheight;
        [XmlAttribute]
        public int spacing;
        [XmlAttribute]
        public int margin;
        public Image image;
    }

    public class Image
    {
        [XmlAttribute]
        public string source;
        [XmlAttribute]
        public int width;
        [XmlAttribute]
        public int height;
        public float Alpha;
        public string Text, FontName;
        public bool IsActive;
    }

    public class tilelayer
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int width;
        [XmlAttribute]
        public int height;
        [XmlElement("data")]
        public string data;

        public string Data
        {
            get {return data;}
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string encodedString = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAF4AAABfAAAAYAAAAGEAAABiAAAAYwAAAGQAAABlAAAAAAAAAAAAAABeAAAAXwAAAGAAAABhAAAAYgAAAGMAAABkAAAAZQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAF4AAABfAAAAYAAAAGEAAABiAAAAYwAAAGQAAABlAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGAAAABkAAAAaAAAAGwAAABwAAAAZAAAAGgAAABsAAAAcAAAAHAAAABsAAAAcAAAAHAAAABkAAAAcAAAAGQAAABoAAAAbAAAAHAAAAB0AAAAQAAAAAwAAAAMAAAAEAAAABQAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAMAAAAEAAAAAwAAAAQAAAAFAAAABgAAAAcAAAAIAAAADwAAABAAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAIAAAAAwAAAAQAAAAFAAAABgAAAAMAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAPAAAA";

            //tilelayer tiles = new tilelayer();
            map map = new map();

            XmlSerializer xml = new XmlSerializer(map.GetType());
            Stream stream = File.Open("Content/scenexml1.xml", FileMode.Open);
            //tiles = (tilelayer)xml.Deserialize(stream);
            map = (map)xml.Deserialize(stream);


            string encodedString = map.Layer[0].Data;
            //string encodedString = map.tilelayer.Data;
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);

            List<Int64> ids64 = new List<Int64>();
            List<int> tile_ids = new List<int>();

            for (int j = 0; j < (20 * 15); j++ )
            {
                int i = j * 4;
                int c = decodedString[i];

                if (c < 0)
                {
                    int cp = 256 + c;
                    tile_ids.Add(cp);
                    //ids64.Add(cp);
                }
                else
                {
                    tile_ids.Add(c);
                    //ids64.Add(c);
                }
                //Console.WriteLine(tile_ids[j]);
                //Console.WriteLine(ids64[j]);

            }

            Console.WriteLine(decodedString);
            Console.WriteLine("******************************************         ********************************");
            Console.WriteLine(map.width);
            Console.WriteLine(map.tileheight);
            Console.WriteLine(map.tilesetinfo.tileheight);
            Console.WriteLine(map.tilesetinfo.image.source);
            //Console.WriteLine(tile_ids);
            //Console.WriteLine(ids64);

            // Keep the console window open in debug mode
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
