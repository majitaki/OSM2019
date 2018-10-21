# -*- coding: utf-8 -*-
import sys
import networkx as nx
import matplotlib.pyplot as plt
import pandas as pd
import glob

def main():
    argv = sys.argv
    argc = len(argv)
    print(argv)
    if(argc != 1):
        print('[Python] ' + 'Arg Error')
        quit()

    csv = glob.glob("./Resources/Output/Working/edge.csv")
    graph_data = pd.read_csv(csv[0])
    G = nx.from_pandas_edgelist(graph_data)
    set_layout(G)

def set_layout(G):
    pos = nx.fruchterman_reingold_layout(G)

    pos_data = pd.DataFrame(pos)
    pos_data = pos_data.T
    pos_data.columns =["x", "y"]
    pos_data.to_csv("./Resources/Output/Working/position.csv")
    print('[Python]' + 'Generate Position')
    with open('./Resources/Output/Working/layout_flag', mode = 'w', encoding = 'utf-8') as fh:
        fh.write("i look at you")

if __name__ == '__main__':
    main()