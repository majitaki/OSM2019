import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd
import os

def main():
    argv = sys.argv
    argc = len(argv)
    print(argv)
    if(argc != 3):
        print('[Python] ' + 'Arg Error')
        quit()
    m = int(argv[1])
    n = int(argv[2])
    generate_graph(m, n)
    print('[Python]' + 'Generate Graph')

def generate_graph(m, n):
    G = nx.hexagonal_lattice_graph(m, n)
    A = nx.to_numpy_matrix(G)
    G = nx.from_numpy_matrix(A)
    node_data = []
    for i in nx.nodes(G):
        node_data.append(i)
    node_data = pd.DataFrame(node_data)
    node_data.to_csv("./Resources/Output/Working/node.csv")
    print('[Python]' + 'Generate Node')
    edge_data = nx.to_pandas_edgelist(G)
    edge_data.to_csv("./Resources/Output/Working/edge.csv")
    print('[Python]' + 'Generate Edge')
    with open('./Resources/Output/Working/flag', mode = 'w', encoding = 'utf-8') as fh:
        fh.write("i look at you")
    #adjacency_data = nx.to_pandas_adjacency(G, dtype=int)
    #adjacency_data.to_csv("./Resources/Output/Working/adjacency.csv")
    #print('[Python]' + 'Generate Adjacency')

if __name__ == '__main__':
    main()