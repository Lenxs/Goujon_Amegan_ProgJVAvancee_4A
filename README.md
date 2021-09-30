# TowerFall 3D

Par Ruben Amegan et Cédric Goujon

Dans le cadre du cours de Programmation jeux vidéo avancée, il nous a été demandé de créer un jeu vidéo avec unity et d'y intégrer une IA "aléatoire". Nous avons décider de créer un jeu inspiré du jeu Towerfall. TowerFall est un jeu vidéo 2D de combat en arène dans lequel jusqu'à quatre joueurs s'affrontent à l'aide de flèches. Dans notre cas nous avons décidé de faire un jeu avec des graphisme 3D, et avec deux joueurs max.

L'IA "aléatoire" prend une décision régulièrement entre sauter, se déplacer latéralement, et tirer. Nous avons commencé à développer une IA plus avancée mais nous n'avons pas eue le temps de la finir.

Pour le contrôle des personnages j'ai décidé d'utiliser un CharacterController, les RigidBody permettent de déplacer les objets de manière réaliste mais ce n'est pas ce qu'on cherche pour un personnage de jeux vidéo la plupart du temps. Alors que le CharacterController est spécialement conçu pour ça.  

Les modèles, textures et animations ont été fait nous mêmes. Il y a une animation pour la course, le tir, le saut des personnages. Il y a aussi une animation de tir pour l'arc. Pour animer les personnages j'ai les Avatar Mask pour animer les jambes et les bras des personnages séparement.


## Ressources

Pour les textes de l'interface utilisateur nous avons utilisé le paquet TextMeshPro.
https://docs.unity3d.com/Manual/com.unity.textmeshpro.html

Voici la liste des liens pour télécharger les sons utilisé dans le projet:
