...
->Greeting
== Greeting ==
+ [Hello?]          ->Intro
+ [(Leave her alone)]  ->END

==Intro==
..I am in deep prayer. What do you want?
    ->MainQuestions
    
    ==MainQuestions==
    + [Where am I?]         ->Where
    + [Who are you?]        ->Who
    + [Can you heal me?]    ->Heal
    + [Nevermind.]          ->END
    
    ==Where==
    I...don't remember. This is why I'm praying to [GODS], to help me remember. Maybe you would know?
        ->WhereQuestions
    ==Who==
    I...don't remember... I think I am the priestess of this temple? Leave me be please, if you have no further questions.
        ->MainQuestions
    ==Heal==
    ...What? I don't know how to heal. Even if I did, I don't heal random strangers.
        ->MainQuestions
        
        ==WhereQuestions==
        + [I don't know]    ->WhereAnswers
        + [...]             
            Nevermind then...You're probably as lost as I am...->MainQuestions

        ==WhereAnswers==
        Figures. Could this be a punishment of [GODS]? Robbing us of our recollection? ->MainQuestions