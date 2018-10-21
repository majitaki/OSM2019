import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd

def main():
    argv = sys.argv
    argc = len(argv)
    print(argv)
    if(argc != 4):
        print('[Python] ' + 'Arg Error')
        quit()
    n = int(argv[1])
    m = int(argv[2])
    seed = int(argv[3])
    generate_graph(n, m, seed)
    print('[Python]' + 'Generate Graph')

def generate_graph(n, m, seed):
    G = nx.gnm_random_graph(n, m, seed)
    node_data = []
    for n in nx.nodes(G):
        node_data.append(n)
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