using System;

namespace Ultimate_Platformer {
    /// <summary>
    /// Manages the world,
    /// The world has multiple <see cref="RegionManager"/>
    /// </summary>
    public class WorldManager
    {
        public string WorldName;
        public int AmountRegions;

        public RegionManager[] RegionManagers;

        public WorldManager()
        {
            string[] lines = System.IO.File.ReadAllLines( @"Content/Data/Maps/World1.map" );

            foreach ( string line in lines )
            {
                if ( line.StartsWith( "|" ) ) continue;

                string[] key = line.Split( ' ' );
                string command = key[ 0 ];
                string value = key[ 1 ];

                switch ( command )
                {
                    case "name":
                        WorldName = value;
                        break;
                    case "regions":
                        AmountRegions = int.Parse( value );
                        break;
                    default:
                        break;
                }
            }
            SetupRegions( AmountRegions, WorldName );
        }

        void SetupRegions( int amountRegions, string worldName )
        {
            RegionManagers = new RegionManager[ amountRegions ];
            for ( int i = 0; i < amountRegions; i++ )
            {
                string[] regiondata = System.IO.File.ReadAllLines( @"Content/Data/Maps/" + worldName + ".Region" + (i + 1) + ".map" );
                RegionManagers[ i ] = new RegionManager( worldName, regiondata );
            }
        }

        void LoadRegion()
        {
            
        }
    }
}
