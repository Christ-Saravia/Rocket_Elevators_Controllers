using System;
using System.Collections.Generic;


namespace src
{
    class Battery
    {
        public List<Column> columnList = new List<Column>();
        public int numfloor;
        public int basements;
        public int colonne;
        public int cage;



        public Battery(int _num_floor, int _basement, int _columns, int _cages)
        {
            this.numfloor = _num_floor;
            this.basements = _basement;
            this.colonne = _columns;
            this.cage = _cages;
            int lieuAppel = 2;
            int x = 1;
            int moyFloorColonne = this.numfloor/(this.colonne -1);
            string colonneActuel;

            while( x <= this.colonne)
            {
                if(x == 1){
                    colonneActuel = "A";
                }
                else if(x < this.colonne){
                    colonneActuel = "B";
                }
                else{
                    colonneActuel= "C";
                }                
                
                switch(colonneActuel){
                    case "A":
                    Console.WriteLine("Bonjour");
                    Column myColumn = new Column(1, _basement, _num_floor);
                    this.columnList.Add(myColumn);
                    break;  
                    case "B":
                    Console.WriteLine("Hi!");
                    Column myColumn2 = new Column(x, lieuAppel,_cages);
                    this.columnList.Add(myColumn2);
                    lieuAppel = moyFloorColonne + 1;
                    moyFloorColonne += this.numfloor/(this.colonne -1);
                    break;
                    case "C":
                    Column myColumn3 = new Column(x, lieuAppel, _cages);
                    this.columnList.Add(myColumn3);
                    Console.WriteLine("Buenos Dias");
                    break;
                    default:
                    Console.WriteLine("ボンジュール");                      // Je dois reflechir pour mon default dans mo switch
                    break;
                }
                var n = x+1;
            }
            Console.WriteLine(lieuAppel);


        }

/*
Pour mon controleur j'ai decide d'adopte l"approche moderne
    Ma methode RequestElevator servira lors de la demande d'ascenseur
        sur un etage ou un sous-sol
*/
        public void RequestElevator()
        {
            Console.WriteLine("IMPORTANT POUR EVALUATION");
        }

/*
    Ma methode servira pour les demandes faites au 1er etage
*/
        public void AssignElevator()
        {
            Console.WriteLine("IMPORTANT POUR EVALUATION");
        }

/*
    Ma methode permettra d'evaluer le nombre d'etage a parcourir
    pour atteindre la destination
*/
        public void DistanceToTravel()
        {

        }

/*
    Ma methode permettra de faire un suivi de l'interaction des listes.
*/

        public void UpdatingMyLists()
        {

        }

/*
    Ici un peu comme mon code de residentiel en python, je vais faire bouger les ascenseurs selon la direction 
*/
        public void Move()
        {

        }
    }

/*
Cette class va me permettre de creer ma liste de floor, elevator, callButton tel que dans mon residentiel en python
*/
    class Column
    {
        public List<Elevator> elevatorList = new List<Elevator>();
        public List<int> listeEtage = new List<int>();
        public List<CallButton> callButtonList = new List<CallButton>();

        public int identification;

        public Column(int identification, int lieuAppel, int _cages)
        {
            this.identification = identification;
            this.listeEtage.Add(1);

            for(int i = lieuAppel; i <= _cages; i++)
            {
                this.listeEtage.Add(1);
            }

            
        }    
    }

/*
Cette class va me permettre de construire mes ascenseurs
*/
    class Elevator
    {
        //List
        public List<int> listeEtage = new List<int>();
        public List<int> whereToEndList = new List<int>();
        public List<int> checkList = new List<int>();

        public int identification;
        public int position = 1;
        public string direction = "UP";

        public string actionDoors = "CLOSED";

        public string checkListDirection = "UP";



        public Elevator(int identification, int numfloor, int lieuAppel)
        {
            this.identification = identification;

            for(int i = lieuAppel; i <= numfloor; i = i + 1)
            {
                this.listeEtage.Add(i);
            }
        }
    }
/*
Cette class va permettre de changer le status des portes.
Idealement, je voudrai permettre de donner un temps pour que les gens rentrent dans l'ascenseur avant la fermeture des portes 
*/
    class Doors
    {
        enum actionDoors
        {
            CLOSED,
            OPENED,
        }
    }

/* 
le controleur prend en charge cet evenement
Evenement 1: En appuyant ce bouton d'appel, la personne demande un ascenseur.
*/
    class CallButton
    {
        public int Etage;
        public string Direction;
        public CallButton(int _etage , string _direction)
        {
            this.Direction = _direction;
            this.Etage = _etage;
        }
    }
    class Display
    {
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
            int num_floor = (int) CommercialBuilding.NumberOfFloor;
            int basement = (int) CommercialBuilding.Basement;
            int columns = (int) CommercialBuilding.Columns;
            int cages = (int) CommercialBuilding.Cages;
         
            Console.WriteLine(num_floor);
            Console.WriteLine(basement);
            Console.WriteLine(columns);
            Console.WriteLine(cages);


        }
            
    }
}
