   M S B A M L   `   `   `  ����F  BAnalyzerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AnalyzerAppAnalyzerApp  5   	       AnalyzerApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:AnalyzerApp     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��400��6)   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF00A7FF��6   � 5      $ 	#FF4E00FF��6    � Offset$ 1Q�61   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Files:��6   
�������6!   �� 5   
   -   5         ��Name$ FileListBox��5      $��1��6'   $��10q�64   � 5   
   -   5         �Name$ ProgressBar��5      $��2��6+   $��	67,0,67,0q�68    � Minimum$ 0Q�6K   	 � Maximum$		 100Q�6W   $y�Visible=�6�   $��18��6�   $��Center=�6�   
 � Value
 6e   �� *
Progress+ �� Mode$ OneWay=���6�   Z� �� 5   
   -   5         ��Name$ AnalyzeButton��5      $��Analyze��6(   $��5��6:   $��5,11,5,8q�6G   ���	#FF09C303
���������6�   Z� 6�   �� 5   
   -   5        $ CancelButton��5      $��Cancel��6'   $��5��68   $��
5,42,5,-23q�6E   $
{�True��6t   ���	#FF00BED6
���������6�   Z� 6�   �� 5   
   -   5         ��Name$ CharactersLabel��5      $��5��6*   $��0,60,10,-48q�67   �� 5    
   -   5        $ 
WordsLabel��5       $��5��6$   $��0,86,10,-75q�61   �� 5!   
   -   5        $ SentencesLabel��5!      $��5��6(   $��0,118,10,-107q�65   $�� ��6L   5"      5#      