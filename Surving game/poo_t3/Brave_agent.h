#ifndef _BRAVE_AGENT_H
#define _BRAVE_AGENT_H

#include "World.h"
#include "Agent.h"

//agentul curajos

class Brave_agent :public Agent {
public:
	Brave_agent(const Position &p) : Agent(p){}
	string get_agent_type()const{ return "#";}
	Position choose_new_position(const World& world) const;
};


#endif