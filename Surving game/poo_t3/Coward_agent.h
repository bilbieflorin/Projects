#ifndef _COWARD_AGENT_H
#define _COWARD_AGENT_H

#include "World.h"
#include "Agent.h"

//agentul curajos


class Coward_agent :public Agent {
public:
	Coward_agent(const Position &p) : Agent(p){}
	string get_agent_type() const{ return "@";}
	Position choose_new_position(const World& world) const;
};


#endif