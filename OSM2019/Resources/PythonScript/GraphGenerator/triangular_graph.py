import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd
import os
from networkx.readwrite import json_graph
import json

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
    G = nx.triangular_lattice_graph(m, n)
    A = nx.to_numpy_matrix(G)
    G = nx.from_numpy_matrix(A)
    data = json_graph.node_link_data(G)
    with open('./Working/graph.json', 'w') as f:
        json.dump(data, f)
    with open('./Working/graph_flag', mode = 'w', encoding = 'utf-8') as fh:
        fh.write("i look at you")

if __name__ == '__main__':
    main()