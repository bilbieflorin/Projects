#include <iostream>

using namespace std;
#include "Agent.h"



void Agent::set_position(const Position& new_position) {
	position.row = new_position.row;
	position.column = new_position.column;
}

Agent::Position Agent::get_position()const {
	return position;
}