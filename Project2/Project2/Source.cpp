#include <iostream>
#include <stack>
#include <vector>
#include <sstream>

using namespace std;

class Tree
{
private:
	string data;
	vector<unique_ptr<Tree>> children = vector<unique_ptr<Tree>>();
public:
	Tree(string data) { this->data = data; }
	int childnum() { return children.size(); };
	string getdata() { return data; }
	Tree* child(int i) { return children[i].get(); };
	void print() { cout << data << endl; };
	void add(Tree* child) { children.push_back(unique_ptr<Tree>(move(child))); };
};

void printall(Tree* root)
{
	if(root)
	{
		for(int i = 0, n = root->childnum(); i < n; i++)
			printall(root->child(i));
		root->print();
	}
}

void prall(Tree* root)
{
	stack<Tree*> toprint;
	toprint.push(root);
	while(toprint.size() != 0)
	{
		Tree* temp = toprint.top();
		temp->print();
		toprint.pop();
		for(int i = 0; i < temp->childnum(); i++)
			toprint.push(temp->child(i));
	}
}

Tree* createNprint(int jPrm, int iPrm)
{
	unique_ptr<Tree> root = move(make_unique<Tree>("root"));
	Tree* temp = root.get();
	for(int j = 0; j < jPrm; j++)
	{
		for(int i = 0; i < iPrm; i++)
		{
			string tstr = temp->getdata() + " " + to_string(i);
			temp->add(new Tree(tstr));
		}
		temp = temp->child(j);
	}
	temp = root.get();

	cout << "Recursion: " << endl;
	printall(temp);

	cout << endl << "Simple:" << endl;
	prall(temp);

	return root.get()->child(1);
}

int main()
{	
	Tree* check = createNprint(3, 3);
}