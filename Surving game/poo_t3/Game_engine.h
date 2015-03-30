#ifndef _GAME_ENGINE_H
#define _GAME_ENGINE_H

#include <string>

using namespace std;

#include "world.h"


class Game {
   World *w;
public :
	Game();
	void run_round();
	void print_state();
	~Game(){delete w;}

};
#endif