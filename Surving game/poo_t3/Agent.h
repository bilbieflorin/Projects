#ifndef AGENT_H_
#define AGENT_H_

#include <string>
using namespace std;

//#include "World.h"

class World;

class Agent {
public:

	// Clasa imbricata pentru a pastra pozitia unui agent
	struct Position {
		int row;
		int column;

		Position(const int r, const int c) {
			row = r;
			column = c;
		}

		bool operator==(const Position &p) {
			if(p.row==row && p.column==column)
				return true;
			return false;
		}
	};



protected:
	// Schimba pozitia agentului
	void set_position(const Position& new_position); 
	struct Position position;
public:

	// Creeaza agentului, initializand variabila position
	// (atentie: trebuie folosite initializers, fiindca
	// clasa Position nu are constructor default)
	Agent(Position p) : position(p) {};

	virtual ~Agent() {};

	// returneaza pozitia curenta a agentului
	Position get_position() const;

	// Returneaza o string care identifica tipul agentului
	virtual string get_agent_type() const =0;
 
	// Intreaba care este noua pozitie dorita
	virtual Position choose_new_position(const World& world) const =0;

	// Permite clasa World sa acceseze metoda set_position
	// Asa garantam ca numai clasa World va avea voie sa modifice pozitia
	// agentului
	friend class World;
};

#endif 
