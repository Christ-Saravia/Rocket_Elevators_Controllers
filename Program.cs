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
            this.numfloor = _num_floor;
            this.basements = _basement;
            this.colonne = _columns;
            this.cage = _cages;
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
        public Column()
        {

        }    
    }

/*
Cette class va me permettre ...
*/
    class Elevator
    {
        public Elevator()
        {

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
        public CallButton()
        {

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
