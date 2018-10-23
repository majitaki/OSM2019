# -*- coding: utf-8 -*-
import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd
import glob
from networkx.readwrite import json_graph
import json

def main():
    argv = sys.argv
    argc = len(argv)
    print(argv)
    if(argc != 1):
        print('[Python] ' + 'Arg Error')
        quit()

    f = open('./Working/graph.json', 'r')
    json_data = json.load(f)
    G = json_graph.node_link_graph(json_data)
    set_layout(G)

def set_layout(G):
    pos = nx.circular_layout(G)

    pos_data = pd.DataFrame(pos)
    pos_data = pos_data.T
    pos_data.columns =["x", "y"]
    pos_data.to_csv("./Working/position.csv")
    print('[Python]' + 'Generate Position')
    with open('./Working/layout_flag', mode = 'w', encoding = 'utf-8') as fh:
        fh.write("i look at you")

if __name__ == '__main__':
    main()