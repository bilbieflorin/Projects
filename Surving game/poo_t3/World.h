#ifndef WORLD_H_
#define WORLD_H_

#include <vector>
#include <string>
using namespace std;

#include "Agent.h"

class World {
private:
	Agent ***agents_matrix;
	vector<Agent*>  agents;
	const int number_of_rows;
	const int number_of_columns;
public:

	// Creeaza lume
	World(const int rows, const int columns);

	virtual ~World();

	int get_number_of_rows() const;
	int get_number_of_cols() const;

	// Adauga un agent la pozitia specificata
	void add_agent(const Agent* agent, const int row, const int column);

	// Sterge agentul din lume (dar nu face delete - trebuie
	// facut de cel care a facut new)
	void remove_agent(const Agent* agent);

	// Returneaza un vector cu toate agentele in lume
	vector<Agent*> get_all_agents() const;

	// Returneaza agentul in pozitia specificata sau 
	// NULL daca nu exista un agent acolo
	Agent* get_agent(const int row, const int column) const;

	// Misca agentul la o noua pozitie, scotandu-l de unde
	// era inainte
	void move_agent(
		Agent* const agent, 
		const int new_row,
		const int new_column);
};

#endif
