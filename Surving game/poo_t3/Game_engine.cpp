#include <iostream>
#include <fstream>
using namespace std;

#include "Agent.h"
#include "game_engine.h"
#include "Brave_agent.h"
#include "Coward_agent.h"
#include "World.h"

//initializam lumea si plasam random cei z agenti pe harta in pozitii diferite
Game::Game(){
	cout<<"Se creaza jocul!!!";
	ifstream F("fisier.txt");
	int x,y,z;
	F >> x >> y;
	w = new World(x,y);
	F >> z;
	for(int i=1; i<=z; i++) {
		Agent::Position p(0,0);
		char c;
		F >> c >> p.row >> p.column;
		Agent *a;
		if(c=='b')
			a = new Brave_agent(p);
		else
			a = new Coward_agent(p);
		w->add_agent(a,p.row,p.column);
	}
}

ostream &operator <<(ostream &out,World &wm) {
	out <<endl <<" ";
	for(int i=0; i<2*wm.get_number_of_cols();i++)
		out << (char)196;
	out << endl;
	for(int i=0; i<wm.get_number_of_rows(); i++) {
		out <<(char)179;
		for(int j=0; j<wm.get_number_of_cols(); j++)
			if(wm.get_agent(i,j)!=NULL)
				out << wm.get_agent(i,j)->get_agent_type()<<" ";
			else
				cout<<" "<<" ";
		out << (char)179;
		out << endl;
	}
	out <<" ";
	for(int i=0; i<2*wm.get_number_of_cols();i++)
		out << (char)196;
	return out;
}
 
void Game::run_round() {
	Agent::Position p(0,0);
	//pentru fiecare agent
	unsigned i = 0;
	vector<Agent*> b;
	b = w->get_all_agents();
	while( i<b.size()){
		Agent *a;
		Agent::Position q(0,0);
		a = b[i];
		p = a->choose_new_position(*w);//il "intrebam" unde vrea sa mearga
		q = a->get_position(); 
		if(!w->get_agent(p.row,p.column)) {//daca e casuta libera merge acolo
			w->move_agent(a,p.row,p.column);
			i++;
		}
		else {//daca nu e libera aruncam un zar  
			if( (rand()%6)%2 ) {//daca numarul de pe zar e impar atunci agentul castiga si al doilea e eliminat 
				Agent *c = w->get_agent(p.row,p.column);
				unsigned j = 0;
				bool ok=false;
				while(j<b.size() && !ok)
					if(c!=b[j])
						j++,
						ok=true;
				w->remove_agent(c);
				w->move_agent(a,p.row,p.column);
				delete c;
				if(j>i)
					i++;
				b=w->get_all_agents();
				}
			else{//daca e par atun ci e eliminat primul agent
				w->remove_agent(a);
				delete a;
				b=w->get_all_agents();
			}
		}
	}
	cout<<endl<<*w;//afisam harta
}

void Game::print_state() {
	cout << *w;
}