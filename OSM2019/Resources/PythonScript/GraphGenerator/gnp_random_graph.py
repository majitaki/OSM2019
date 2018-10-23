import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd
from networkx.readwrite import json_graph
import json

def main():
    argv = sys.argv
    argc = len(argv)
    print(argv)
    if(argc != 4):
        print('[Python] ' + 'Arg Error')
        quit()
    n = int(argv[1])
    p = float(argv[2])
    seed = int(argv[3])
    generate_graph(n, p, seed)
    print('[Python]' + 'Generate Graph')

def generate_graph(n, p, seed):
    G = nx.gnp_random_graph(n, p, seed)
    data = json_graph.node_link_data(G)
    with open('./Working/graph.json', 'w') as f:
        json.dump(data, f)
    with open('./Working/graph_flag', mode = 'w', encoding = 'utf-8') as fh:
        fh.write("i look at you")

if __name__ == '__main__':
    main()