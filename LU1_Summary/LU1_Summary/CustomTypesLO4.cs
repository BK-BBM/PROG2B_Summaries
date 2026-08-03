using System;
using System.Collections.Generic;
using System.Text;

namespace LU1_Summary
{
    public class CustomTypesLO4
    {
        public enum GameLevel { Easy, Medium, Hard }
        public struct GamePrice
        {
            public decimal Amount;
        }
        public class Game
        {
            public int gameId;
            public GameLevel level;
            public GamePrice gamePrice;
        }
     
         /* Classes -> reference type
         * structs -> value types (good for immutable data and they're copied by value)
         *         -> If you've got "small" data that needs to be modelled and will never
         *         change (use a struct) 
         * enums (enumerators) -> used for constant values       
         * record -> a reference type (used for value-based data)
         */
    }
}
