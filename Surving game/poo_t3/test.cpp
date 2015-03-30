#include <iostream>
#include "World.h"
#include "Brave_agent.h"

using namespace std;

int  main() {

	World *w;


	w = new World(10,10);
	Agent::Position p(1,1);
	Brave_agent *b;
	b=new Brave_agent(p);
	w->add_agent(b,p.row,p.column);
	system("pause");
	delete w;
	return 0;
}