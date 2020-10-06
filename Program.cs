using System;

namespace src
{
    class Battery
    {
        public int numfloor;
        public int basements;
        public int colonne;
        public int cage;

        public Battery(int _num_floor, int _basement, int _columns, int _cages)
        {
            numfloor = _num_floor;
            basements = _basement;
            colonne = _columns;
            cage = _cages;

            string[] columnList = new string[colonne];
            int[,] elevatorList = new int[colonne,cage];

            for(int i = 0; i < colonne; i++){
                columnList.SetValue("A", i);
                Console.WriteLine( "[Colonne]:   {0}", columnList.GetValue( i ));
                for(int e = 0; e < cage; e++){
                    elevatorList.SetValue(e+1, i, e);
                    Console.WriteLine("[Ascenseur]: {0}", elevatorList.GetValue(i,e));         
                }            
            }
        }
    }
    class Column
    {
        public string size = "grand";
    }

    class Elevator
    {
        public string emotion = "heureux";
    }

    class Doors
    {
        public string sport = "hockey";
    }
    class Display
    {
        public string ville = "Montreal";
    }
    class Program
    {   
        enum CommercialBuilding
        {
            NumberOfFloor = 66,      
            Basement = 6,           
            Columns = 4,            
            Cages = 5,                      
        }

        enum SectionEtageCouvertA
        {
            L,
            B1,
            B2,
            B3,
            B4,
            B5,
            B6,

        }
        enum SectionEtageCouvertB
        {
            L,
            L2,
            L3,
            L4,
            L5,
            L6,
            L7,
            L8,
            L9,
            L10,
            L11,
            L12,
            L13,
            L14,
            L15,
            L16,
            L17,
            L18,
            L19,
            L20,
        }

        enum SectionEtageCouvertC
        {
            L,
            L21,
            L22,
            L23,
            L24,
            L25,
            L26,
            L27,
            L28,
            L29,
            L30,
            L31,
            L32,
            L33,
            L34,
            L35,
            L36,
            L37,
            L38,
            L39,
            L40
        }
        enum SectionEtageCouvertD
        {
            L,
            L41,
            L42,
            L43,
            L44,
            L45,
            L46,
            L47,
            L48,
            L49,
            L50,
            L51,
            L52,
            L53,
            L54,
            L55,
            L56,
            L57,
            L58,
            L59,
            L60,
        }
        static void Main(string[] args)
        {
            sbyte num_floor = (sbyte) CommercialBuilding.NumberOfFloor;
            sbyte basement = (sbyte) CommercialBuilding.Basement;
            sbyte columns = (sbyte) CommercialBuilding.Columns;
            sbyte cages = (sbyte) CommercialBuilding.Cages;

            

            Battery myObj = new Battery(num_floor, basement, columns, cages);
            Console.WriteLine(myObj.basements);
            Console.WriteLine(myObj.numfloor);
            Console.WriteLine(myObj.cage);
        }
            
    }
}
