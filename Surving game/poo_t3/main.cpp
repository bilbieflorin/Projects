#include <iostream>
#include <time.h>

using namespace std;

#include "game_engine.h"
#include "world.h"

int main() {
	srand(time(NULL));
	Game G;
	G.print_state();//afisam starea initiala
	int i,r=0;
	cout<<endl;
 	system("pause");
	do {//rulam runda cat timp doreste utilizatorul 
		r++;
		cout<<"\nRunda "<< r;
		G.run_round();
		cout << "\nDoriti sa continuati? 0-DA/1-NU\n=>";
		cin >> i;
	}while(i == 0);
	cout << endl;
	return 0;
}