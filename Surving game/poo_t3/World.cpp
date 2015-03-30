#include <iostream>
#include <algorithm>

using namespace std;

#include "World.h"
#include "Agent.h"

World::World(const int rows, const int columns):number_of_rows(rows),number_of_columns(columns) { 
	agents_matrix = new Agent**[rows];
	for(int i=0; i<=rows ;i++)
		agents_matrix[i] = new Agent*[columns];
	for(int i=0;i<rows;i++)
		for(int j=0;j<columns;j++)
			agents_matrix[i][j]=NULL;
}
	
World::~World() {
	while(agents.size()>0)
		agents.pop_back();
	for(int i=0;i<number_of_rows;i++)
		for(int j=0;j<number_of_columns;j++)
			if(agents_matrix[i][j]!=NULL)
				delete agents_matrix[i][j],
				agents_matrix[i][j]=NULL;
	for(int i=0; i < number_of_rows; i++)
		delete[] agents_matrix[i],
		agents_matrix[i]=NULL;
	//delete[] agents_matrix;
}

void World::add_agent(const Agent *ag,const int row,const int column) {
	agents_matrix[row][column] =(Agent*) ag;
	bool ok = true;
	for(int i=0;i<agents.size();i++)
	     if(ag==agents[i])
			 ok = false;
	if(ok)
		agents.push_back ((Agent*)ag); 
}
 
void World::remove_agent(const Agent *agent) {
	agents_matrix[agent->position.row][agent->position.column] = NULL;
	unsigned i = 0;
	while(i < agents.size() && agent!=agents[i])
		i++;
	for(unsigned j=i;j<agents.size()-1;j++)
		agents[j] = agents[j+1];
	agents.pop_back();
}

int World::get_number_of_rows() const {
	return number_of_rows;
}

int World::get_number_of_cols() const {
	return number_of_columns;
}

Agent* World::get_agent(const int row,const int column) const{
	return agents_matrix[row][column];
}

vector<Agent*> World::get_all_agents()const {
	return agents;
}

void  World::move_agent( Agent* const agent, const int new_row, const int new_column) {
	Agent::Position p(new_row,new_column),
		q = agent->get_position();
	agents_matrix[q.row][q.column] = NULL;
	agent->set_position(p);
	agents_matrix[p.row][p.column] = agent;
}
