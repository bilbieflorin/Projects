#include <iostream>

using namespace std;

#include "Brave_agent.h"
#include "World.h"


Agent::Position Brave_agent::choose_new_position(const World& world) const {
	Position p(0,0);
	Position border1(position.row-2,position.column-2),border2(position.row+2,position.column+2);
	int n = world.get_number_of_rows();
	int m = world.get_number_of_cols();
	int X[2]={2,-2} ,Y[2]={2,-2};//vectori de stabilire a urmatoarei pozitii in matrice
	//fixam limitele arei vizuale a agentului
	bool ok = false;
	int i=border1.row,j;
	//cautam un agent in zona vizibila
	while(!ok && i<=border2.row) {
		j=border1.column;
		p.row = i;
		while(!ok && j<=border2.column) {
		    p.column = j;
			if(p.row>=0 && p.row<n && p.column>=0 && p.column<m && !(p==position))
				if(world.get_agent(p.row,p.column))
					ok=true;//daca gasim atunci avem urmatoarea locatie a agentului curajos
			j++;
		}
		i++;
	}
   if(!ok)//daca nu gasim alegem random o pozitie la distanta  2 de cea actuala
	    while(!ok) {
			int i,j;
			i= rand()%2;
			j= rand()%2;
			p.row = position.row+X[i];
			p.column = position.column+Y[j];
			if(p.row>=0 && p.row<n && p.column>=0 && p.column<m )
				ok = true;
		}
   
	
return p;
}