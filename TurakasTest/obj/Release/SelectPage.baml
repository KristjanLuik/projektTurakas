   M S B A M L   `   `   `  ����   TurakasTest    TurakasTest.SelectPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  
SelectPage��.���  � RowDefinitions �� $	��140 ��� $��* �.+��  � ColumnDefinitions �� $
��Auto ��� $��* �.+��� -    ��Name$ 	pageTitle��$��WPF TURAKAS��$��1��$��Center=�#��� ��#��� ���� -   $ username��$��1��$
��Left=�$��
723,10,0,0q�$
��Wrap=�$��	TextBlock��$��36(�$	��120��$	��513��$
��Bold"�$��Center=�#��� �����	#FFF31414� -    �Name$ lobby��$��1��$��1������  �� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+��� -    ��Name$ btnhelp��$��0��$��0��.���� $��Assets/help.png��$
��fill=��� -   $ stkStart��$��0��$��1��.���� $��Assets/start.png��$
��fill=��� -   $ stkJoin��$��0��$��2��.���� $��Assets/join.png��$
��fill=��� -    ��Name$ 	frameMain��$��1��$��0��$��2��$y�	Collapsed=�.+��� -	   $ 
stkPicture��.+�(� -
    (�Name$ 
vbxPicture��$	��419��.���� $��Assets/images/jr.png��$
��None=��� -   $ 
frameStart��$��1��$��0��$��3��$y�	Collapsed=�$��0,0,0,10q�.+��� .+��� -    ��Name$ lbxAddToGame��	 �� SelectionMode$	 Multiple=����� ���� .R��� ���� *Name+�� -   $ btnPick��$��Choose���� -   $ 	frameJoin��$��1��$��0��$��3��$y�Visible=�$��0,0,0,10q�.+��� .+��� -   $ lbxJoinGame������ ���� .R��� ���� *Owner+�� -   $ btnPickGame��$��Choose���� -   $ 	frameWait��$��1��$��0��$��3��$y�Visible=�$��0,0,0,10q�.+��� .+��� -   $ lbxWait������ ���� .R��� ���� *Name+