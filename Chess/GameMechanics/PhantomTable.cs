using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chess.ChessFigures;

namespace Chess.GameMechanics
{
    class PhantomTable
    {
        static int PhantomWhiteKingIndex;
       static  int PhantomBlackKingIndex;
        public static bool CanThisBlock(int i, string id,title[] Board, int BlackKingIndex, int WhiteKingIndex)
        {
            int[] displaySelectable;
            switch (id)
            {
                case "pawn":
                    displaySelectable = pawn.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board,BlackKingIndex, WhiteKingIndex);

                case "queen":
                    displaySelectable = queen.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board, BlackKingIndex, WhiteKingIndex);

                case "king":
                    displaySelectable = king.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board, BlackKingIndex, WhiteKingIndex);


                case "castle":
                    displaySelectable = castle.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board, BlackKingIndex, WhiteKingIndex);

                case "horse":
                    displaySelectable = horse.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board, BlackKingIndex, WhiteKingIndex);

                case "bishop":
                    displaySelectable = bishop.CanMove(Board, i, Board[i].colorOfFigure);
                    return CanThisFigureBlock(displaySelectable, i, Board, BlackKingIndex, WhiteKingIndex);

            }
            return false;
        }
        
        private static bool CanThisFigureBlock(int[] GoArr, int j,title[] Board, int BlackKingIndex, int WhiteKingIndex)
        {
            title[] phantomDeck = Board;
            for (int i = 0; i < GoArr.Length; i++)
            {
                if (FigureMover(GoArr[i], j, phantomDeck, BlackKingIndex, WhiteKingIndex)) return true;
            }
            return false;
        }
     
        public static bool FigureMover(int newPos, int prevPos, title[] arr, int BlackKingIndex, int WhiteKingIndex)
        {
            PhantomWhiteKingIndex = Table.whiteKingIndex;
            PhantomBlackKingIndex = Table.blackKingIndex;
            string colorF = arr[newPos].colorOfFigure;
            string ID = arr[newPos].Id_Of_Figure;
            string FC = arr[newPos].firstColorOfFigure;
            TwoFigureChanger(newPos, prevPos, arr);
            if (calculateIfProtected(arr[newPos].colorOfFigure, arr, PhantomBlackKingIndex, PhantomWhiteKingIndex))
            {
                arr[prevPos].Id_Of_Figure = arr[newPos].Id_Of_Figure;
                arr[prevPos].colorOfFigure = arr[newPos].colorOfFigure;
                arr[prevPos].firstColorOfFigure = arr[newPos].firstColorOfFigure;
                arr[newPos].Id_Of_Figure = ID;
                arr[newPos].colorOfFigure = colorF;
                arr[newPos].firstColorOfFigure = FC;

                return false;
            }
            arr[prevPos].Id_Of_Figure = arr[newPos].Id_Of_Figure;
            arr[prevPos].colorOfFigure = arr[newPos].colorOfFigure;
            arr[prevPos].firstColorOfFigure = arr[newPos].firstColorOfFigure;
            arr[newPos].Id_Of_Figure = ID;
            arr[newPos].colorOfFigure = colorF;
            arr[newPos].firstColorOfFigure = FC;

            return true;

        }
   
        public static bool calculateIfProtected(string color, title[] arr, int BlackKingIndex, int WhiteKingIndex)
        {

            if (color == "white")
            {
                //   if (Table.isCheckmateWhite == false) return false;
                return TableSelectors.Plunk("black", arr, WhiteKingIndex);

            }
            else
            {
                //   if (Table.isCheckmateBlack == false) return false;
                return TableSelectors.Plunk("white", arr, BlackKingIndex);

            }

        }
        public static void TwoFigureChanger(int newPos, int prevPos, title[] arr)
        {
            arr[newPos].Id_Of_Figure = arr[prevPos].Id_Of_Figure;
            arr[newPos].colorOfFigure = arr[prevPos].colorOfFigure;
            arr[newPos].firstColorOfFigure = arr[prevPos].firstColorOfFigure;
            arr[prevPos].Id_Of_Figure = " ";
            arr[prevPos].colorOfFigure = " ";
            arr[prevPos].firstColorOfFigure = " ";
            if (arr[newPos].Id_Of_Figure == "king")
            {
                if (arr[newPos].colorOfFigure == "white") PhantomWhiteKingIndex = newPos;
                if (arr[newPos].colorOfFigure == "black") PhantomBlackKingIndex = newPos;
            }
        }
  
    }
}
