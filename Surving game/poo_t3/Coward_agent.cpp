#include <iostream>
#include <time.h>

using namespace std;

#include "Coward_agent.h"
#include "World.h"




Agent::Position Coward_agent::choose_new_position(const World& world) const {
	Position p(0,0);
	Position border1(position.row-1,position.column-1), border2(position.row+1, position.column+1);
	int n = world.get_number_of_rows();
	int m = world.get_number_of_cols();
	int X[2]={-1,1} ,Y[2]={-1,1};
	bool ok = false;
	int i=border1.row,j;
	//cautam un agent in matrice 3x3 centrata in pozitia agentului curent 
	while(!ok && i<=border2.row) {
		j=border1.column;
		p.row = i;
		while(!ok && j<=border2.row) {
		    p.column = j;
			if(!(p.row>=0 && p.row<n && p.column>=0 && p.column<m) || p==position)
				ok = false;    
			else
				if(world.get_agent(p.row,p.column))
					ok=true;
			j++;
		}
		i++;
	}
	//daca gasim incercam deplasarea in directia opusa cu 1
	if(ok && !(position.row==0 || position.row==n-1 || position.column==m-1 || position.column==0)){
		p.row = position.row+position.row-p.row;
		p.column = position.column+position.column-p.column;
		if(!world.get_agent(p.row,p.column))
			return p;
		else
			do{//daca nu putem merge in directia opusa alegem random unloc liber in matricea vizibila 
				i= rand()%2;
				j= rand()%2;
				p.row = position.row+X[i];
				p.column = position.column+Y[j];
			}
			while(p.row<0 || p.row>n || p.column<0 || p.column>m || world.get_agent(p.row,p.column));
	  }
	else
		if(!ok){//daca nu gasi agent atunci alegem o pozitie random
			while(!ok) {
				i= rand()%2;
				j= rand()%2;
				p.row = position.column+X[i];
				p.column = position.column+Y[j];
				if(p.row>=0 && p.row<n && p.column>=0 && p.column<m && !world.get_agent(p.row,p.column))
					ok = true;
				}
		}

	
	
return p;
}