    using System;
    using System.Linq;

    namespace Scratchpad
    {
    
        public static class QueensAttack {

            private enum Direction {
                 // Horizontals
                Left = 0,
                Right = 1,
                // Verticals
                Up = 2,
                Down = 3,
                //Diagonals
                RightDown = 4,
                RightUp = 5,
                LeftDown = 6,
                LeftUp = 7,
            }

            /*
            * https://www.hackerrank.com/challenges/queens-attack-2/problem
            */
            public static int queensAttack(int dimension, int totalObs, int queenY, int queenX, int[][] obstacles) {
                // number of sqaures from queen in a given direction until she is blocked
                int[] distanceUntilBlocked = new int[Enum.GetValues(typeof(Direction)).Length];

                PopulateDistances(distanceUntilBlocked, dimension, queenX, queenY);

                // Only one square on board
                if(dimension == 1)
                    return 0;

                for(int i = 0; i < totalObs; i++){
                    int obsY = obstacles[i][0];
                    int obsX = obstacles[i][1];
                    
                    if(obsX == queenX) {
                        if(obsY > queenY) {
                            int distanceFromQueen = obsY - queenY;
                            if(distanceUntilBlocked[(int)Direction.Up] >= distanceFromQueen) {
                                distanceUntilBlocked[(int)Direction.Up] = distanceFromQueen - 1;
                            } 
                        } else {
                            int distanceFromQueen = queenY - obsY;
                            if(distanceUntilBlocked[(int)Direction.Down] >= distanceFromQueen) {
                                distanceUntilBlocked[(int)Direction.Down] = distanceFromQueen - 1;
                            } 
                        }
                    } else if(obsY == queenY) {
                        if(obsX > queenX) {
                            int distanceFromQueen = obsX - queenX;
                            if(distanceUntilBlocked[(int)Direction.Right] >= distanceFromQueen) {
                                distanceUntilBlocked[(int)Direction.Right] = distanceFromQueen - 1;
                            }
                        } else {
                            int distanceFromQueen = queenX - obsX;
                            if(distanceUntilBlocked[(int)Direction.Left] >= distanceFromQueen) {
                                distanceUntilBlocked[(int)Direction.Left] = distanceFromQueen - 1;
                            }
                        }
                    } else if(obsY > queenY && obsX > queenX) {
                        int yDiff = obsY - queenY;
                        int xDiff = obsX - queenX;

                        if(xDiff == yDiff && distanceUntilBlocked[(int)Direction.RightUp] >= xDiff){
                            distanceUntilBlocked[(int)Direction.RightUp] = xDiff - 1;
                        }
                    } else if(obsY < queenY && obsX > queenX) {
                        int yDiff = queenY - obsY;
                        int xDiff = obsX - queenX;

                        if(xDiff == yDiff && distanceUntilBlocked[(int)Direction.RightDown] >= xDiff) {
                            distanceUntilBlocked[(int)Direction.RightDown] = xDiff - 1;
                        }
                    } else if(obsY > queenY && obsX < queenX) {
                        int yDiff = obsY - queenY;
                        int xDiff = queenX - obsX;

                        if(xDiff == yDiff && distanceUntilBlocked[(int)Direction.LeftUp] >= xDiff){
                            distanceUntilBlocked[(int)Direction.LeftUp] = xDiff - 1;
                        }
                    } else if(obsY < queenY && obsX < queenX) {
                        int yDiff = queenY - obsY;
                        int xDiff = queenX - obsX;

                        if(xDiff == yDiff && distanceUntilBlocked[(int)Direction.LeftDown] >= xDiff){
                            distanceUntilBlocked[(int)Direction.LeftDown] = xDiff - 1;
                        }
                    }
        
                }

                return distanceUntilBlocked.Sum();

            }

            // Populates all directions to distance to edge of board
            private static void PopulateDistances(int[] distanceArr, int dimension, int queenX, int queenY) {
                distanceArr[(int)Direction.Up] = dimension - queenY;
                distanceArr[(int)Direction.Down] = dimension - distanceArr[(int)Direction.Up] - 1;

                distanceArr[(int)Direction.Right] = dimension - queenX;
                distanceArr[(int)Direction.Left] = dimension - distanceArr[(int)Direction.Right] - 1;

                if(distanceArr[(int)Direction.Up] == distanceArr[(int)Direction.Right]
                   || distanceArr[(int)Direction.Up] > distanceArr[(int)Direction.Right] ) {
                    distanceArr[(int)Direction.RightUp] = distanceArr[(int)Direction.Right];
                } else {
                    distanceArr[(int)Direction.RightUp] = distanceArr[(int)Direction.Up];
                }

                if(distanceArr[(int)Direction.Down] == distanceArr[(int)Direction.Right]
                   || distanceArr[(int)Direction.Down] > distanceArr[(int)Direction.Right] ) {
                    distanceArr[(int)Direction.RightDown] = distanceArr[(int)Direction.Right];
                } else {
                    distanceArr[(int)Direction.RightDown] = distanceArr[(int)Direction.Down];
                }

                if(distanceArr[(int)Direction.Down] == distanceArr[(int)Direction.Left]
                   || distanceArr[(int)Direction.Down] > distanceArr[(int)Direction.Left] ) {
                    distanceArr[(int)Direction.LeftDown] = distanceArr[(int)Direction.Left];
                } else {
                    distanceArr[(int)Direction.LeftDown] = distanceArr[(int)Direction.Down];
                }

                if(distanceArr[(int)Direction.Up] == distanceArr[(int)Direction.Left]
                   || distanceArr[(int)Direction.Up] > distanceArr[(int)Direction.Left] ) {
                    distanceArr[(int)Direction.LeftUp] = distanceArr[(int)Direction.Left];
                } else {
                    distanceArr[(int)Direction.LeftUp] = distanceArr[(int)Direction.Up];
                }
            }
        }
    }
