   M S B A M L   `   `   `  ����P  LspeakerIdentification, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:speakerIdentificationspeakerIdentification  5   	   &     speakerIdentification.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:speakerIdentification     5   	   -   5          � Title$  
MainWindow��5   	   $��574.059��6   $��995.084��6-   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��320* �6   �� 5      $
��218* �6   �� 5      $
��284* �6   �� 5      $
��165* �6    � RowDefinitions 5   
   �� 5      $	��24* �6   �� 5      $	��23* �6   �� 5      $	��38* �6   �� 5      $	��37* �6   �� 5      $	��28* �6   �� 5      $	��42* �6   �� 5      $	��51* �6   �� 5      $	��49* �6   �� 5      $	��32* �6   �� 5      $	��40* �6   �� 5      $	��48* �6   �� 5      $	��69* �6   �� 5      $	��36* �6   �� 5      $
��Auto �6   $��27��6*   .+�5    
   �� -   5         ��Name$ _speakersListView��5       $��1��6.   $
��Left=�6;   $	��314��6V   $��0,3,0,0q�6b   $��12��6s    �� View 5!      �� 5"      .P�5#      �� $��
Speaker ID��6%    �� Width$	 300��69    �� DisplayMemberBinding 6E   �� *	ProfileId+�� 5$      $��Enrollment Status��6%    6@   �� *EnrollmentStatus+�� 5%      $��Remaining Enrollment Time��6%    6H   �� *" RemainingEnrollmentSpeechSeconds+5&      5(      �� 5)   
   $��13��6   $��0,5,10,0q�6#    �� Orientation$ 
Horizontal=�65   .+�5*      �� $	��170��6   -+Click on [Update List] to load all profiles6Q   �� 5+      -   5         ��Name$ 
_UpdateBtn��5+      $��5,0,0,0q�6)   $��_Update List��6:   6Q   5,      �� 5-   
   $��0��6   $��0��6!   $��	5,0,198,2q�61   $
��Bold"�6D   Enroll Speakers6g    � 5.   
   -   5        	  �Name$	 _speakersListFrame��5.      $��13��6,   $��0��6:   $��
5, 5, 0, 0q�6J   
  � NavigationUIVisibility$
 Hidden=�6^   �� 5/   
   $��6,0,0,2q�6   $
��Bold"�6%   $��WrapWithOverflow=�67   $��2��6W   Step 1: Create New Profiles6�   �� 50   
   $��2��6   $��6,22,0,3q�6$   $��WrapWithOverflow=�66   $��2��6V   (&Please click [Add] to add new profiles6�   �� 51   
   $��2��6   $ 
Horizontal=�6%   $��2��6>   $��6,0,0,0q�6K   .+�52      �� $��5,9,0,0q�6   �� 6*   $�Locale��6.   6?   �� 53      -   5         ��Name$ 
_localeCmb��53      $��5,9,0,10q�6+    �� SelectedIndex$ 0��6=    J� RenderTransformOrigin$ 0.552,-0.404&�6O   .@�54      �� $��en-us��6   �� 55      $��zh-cn��6   56      57      �� 58   
   -   5        $ _addBtn��58      $��3��6"   $��	10,3,39,7q�62   $��2��6[   .��59      �� $��WrapWithOverflow=�6    �� TextAlignment$ Center=�68   Add6T   5:      �� 5;   
   $��2��6   $
��Bold"�6$   $��WrapWithOverflow=�66   $��3��6V   $��6,0,0,0q�6c   Step 2: Enroll Profile6�   �� 5<   
   $��6,0,0,0q�6   $��WrapWithOverflow=�6%   $��2��6E   $��4��6U   :8Select a profile from the list on the left to enroll it.6�   �� 5=   
   $��2��6   $��6,0,0,4q�6%   $��WrapWithOverflow=�66   $��5��6V   53Click [Load File] to specify a file to enroll with.6�   �� 5>   
   -   5        $ _loadFileBtn��5>      $��3��6'   $��	10,6,39,4q�67   $��5��6e   .��5?      �� $��WrapWithOverflow=�6   $ Center=�68   	Load File6Z   5@      �� 5A   
   $��2��6   $��6,1,0,5q�6$   $��WrapWithOverflow=�65   $��6��6U   +)Click [Enroll] to perform the enrollment.6�   �� 5B   
   -   5        $ 
_enrollBtn��5B      $��3��6%   $��
10,9,39,10q�65   $��6��6b   .��5C      �� $��WrapWithOverflow=�6   $ Center=�68   Enroll6W   5D      �� 5E   
   $��2��6   $��6,0,0,10q�6$   $��WrapWithOverflow=�66   $��7��6V   MKOr, click [Enroll Short Audio] to skip the enrollment duration requirement.6�   �� 5F   
   -	   5        $ _enrollShortAudioBtn��5F      $��
10,10,39,0q�6/   $��3��6\   $��29��6l   $	��Top=�6x   $��7��6�   .��5G      �� $��WrapWithOverflow=�6   $ Center=�68   Enroll Short Audio6c   5H      �� 5J   
   $��8��6   $��6,0,0,0q�6!   $
��Bold"�62   $��WrapWithOverflow=�6D   $��2��6d    Step 3: Perform Identification6�   �� 5K   
   $��9��6   $��2��6!   $��6,0,0,0q�61   $��WrapWithOverflow=�6B   B@Select some profiles from the list on the left to identify from.6�   �� 5L   
   $��10��6   $��2��6"   $��5,5,0,0q�62   $��WrapWithOverflow=�6C   1/Click [Identify] to perform the identification.6�   �� 5M   
   -
   5        $ _identifyBtn��5M      $��10��6'   $��3��65   $��10,10,39,10q�6E   .��5N      �� $��WrapWithOverflow=�6   $ Center=�68   
Identify6Y   5O      �� 5P   
   $��11��6   $��2��6"   $��	6,10,0,21q�62   $��WrapWithOverflow=�6E   SQOr, click [Identify Short Audio] to skip the identification duration requirement.6�   �� 5Q   
   -   5        $ _identifyShortAudioBtn��5Q      $��11��61   $��3��6?   $��
10,20,39,7q�6O   .��5R      �� $��WrapWithOverflow=�6   $ Center=�68   Identify Short Audio6e   5S      �� 5T   
   -   5         ��Name$" _identificationResultStckPnl��5T      $��11��6;   $��2��6I   $ Vertical=�6Y   $��Center=�6p   $��Center=�6�   $y�Hidden=�6�   $��	6,62,6,20q�6�   $��3��6�   $	��272��6�   $��50��6�   .+�5U      �� $ 
Horizontal=�6   $��30��62   .+�5V      �� $��
5, 5, 0, 0q�6   Identified speaker:6E   �� 5W      -   5         ��Name$! _identificationResultTxtBlk��5W      $��
5, 5, 0, 0q�6A   $
��Bold"�6U   5X      �� 5Y      $ 
Horizontal=�6   .+�5Z      �� $��
5, 5, 0, 0q�6   Confidence:6=   �� 5[      -   5        $% _identificationConfidenceTxtBlk��5[      $��
5, 5, 0, 0q�6E   $
��Bold"�6Y   5\      5]      �� 5^   
   -   5        $ btnStartRecording��5^      $��	Recording��6,   $��1��6@   $
��Left=�6P   $��0,3,0,0q�6k   $��2��6|   $	��Top=�6�   $	��208��6�   $��35��6�   6�   �� 5_   
   -   5        $ btnStopRecording��5_      $��Stop Recording��6+   $��1��6D   $
��Left=�6T   $��0,6,0,0q�6o   $��5��6�   $	��Top=�6�   $	��208��6�   $��32��6�   6�   5b      5c      