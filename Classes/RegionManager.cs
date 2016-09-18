using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ultimate_Platformer {
    /// <summary>
    /// One region in <see cref="WorldManager"/>,
    /// has multiple <see cref="Tile"/>
    /// </summary>
    public class RegionManager
    {
        private string _worldName;

        public RegionManager( string worldName, IEnumerable< string > data )
        {
            _worldName = worldName;

            //foreach ( string s in data )
            //{
            //    
            //}
        }
    }
}
