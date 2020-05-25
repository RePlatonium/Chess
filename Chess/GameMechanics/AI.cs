using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using SFML.Window;
using Chess.GameMechanics;
using Chess.ChessFigures;
namespace Chess.GameMechanics
{
    class AI
    {
       static title[] table;
        public  static void MakeAEasyMove(RenderWindow win)
        {
            
            table = Table.actualDeck;
           
            int[] move = AICalculations_2.CalculateBestMove(table);
            if (move[1] == move[0])
            {
                MakeARandoMmove(win);
                return;
            }
            TableSelectors.FigureMover(move[1], move[0], win);
            TableSelectors.ChangeFigureTargets(Table.actualDeck[move[1]], move[1]);
        }
       public static void MakeARandoMmove(RenderWindow win)
        {
            
            bool selectedIndex = false;
            table = Table.actualDeck;
            
            while (selectedIndex == false)
            {
                Random rand = new Random();
                int index = rand.Next(0, 64);
                if (table[index].colorOfFigure != "black") continue;
                int[] indexes = CanMove(index);
                if (indexes.Length==0 || indexes[0] == -1) continue;
                int newTitle = ChooseRandomly(indexes);
                if (!TableSelectors.FigureMover(newTitle, index, win)) continue;
                selectedIndex = true;
            }

        }
       static int[] CanMove(int i)
        {
            int[] arr = new int[] { -1 };
            switch (table[i].Id_Of_Figure)
            {
                case "pawn":
                   arr= pawn.CanMove(table, i, table[i].colorOfFigure);
                    break;
                case "queen":
                    arr = queen.CanMove(table, i, table[i].colorOfFigure);
                    break;
                case "king":
                    arr = king.CanMove(table, i, table[i].colorOfFigure);
                    break;
                case "horse":
                    arr = horse.CanMove(table, i, table[i].colorOfFigure);
                    break;
                case "bishop":
                    arr = bishop.CanMove(table, i, table[i].colorOfFigure);
                    break;
                case "castle":
                    arr = castle.CanMove(table, i, table[i].colorOfFigure);
                    break;
             
            }
            return arr;
        }
        static int ChooseRandomly(int[] arr)
        {
            int MaxValue = arr.Max();


            while (true)
            {
                Random rand = new Random();
                int stopP = rand.Next(0, 64);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (stopP == arr[i]) return arr[i];
                }
            }
        }
        

    }
}
