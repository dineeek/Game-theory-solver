# Game Theory Solver

![alt text](https://github.com/dineeek/GameTheory/blob/master/OI2GameTheory/Resources/GameTheorySolver.png)

Game theory problem solver through linear programming using simplex algorithm. This project is done for master degree in Faculty of Organization and Informatics, Varaždin.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

For installing this software you can find Publish directory in this repository or open in Visual Studio.

## Application functionality

This application is based on zero sum games for example:

_A\B_|_Y1_Y2_Y3 <br/>
X1 |    2 5 -3 <br/>
X2 |    -1 3  2 <br/>
X3 |     3-5 -4 <br/>

Player A has 3 strategies - X1, X2, X3 while player B also has 3 strategies - Y1, Y2, Y3. 

Every strategy carry some payoff for player, for example if player A plays strategy X1 and player B strategy Y1 - player A has payoff of 2 units (player B loses 2 units). In other side, if player A plays strategy X1 and player B strategy Y3 - player B has payoff of 3 units (player A loses 3 units).

This application either for player A or player B makes calculation using linear programming, apropos simplex algorithm to find value of entered game and the percentage of each player's playing strategy. 

Result for upper game would be: <br/>
The game value: 0,2<br/>
Probability of playing a player's strategy: <br/>
Player A: X1 = 0%   X2 = 70%   X3 = 30%   <br/>
Player B: Y1 = 60%   Y2 = 0%   Y3 = 40%   <br/>

In addition application can create model of problem for entered game indicating the entire linear programming process. Also, user can see all computation made for simplex allgorithm.

## Built With

* [C#](http://www.dropwizard.io/1.0.2/docs/) - Windows forms used

## Authors

* **Dino Kliček** - *Initial work* - [dineeek](https://github.com/dineeek)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

