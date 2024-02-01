# TEAM A LAB 1
### Members:
Sanxing Liu\
Xiaojing Zhang\
Ethan Worden\
Richard Montgomery\
Beth Gallatin

The code word detail as follows:
Codebook work space: [Link to Google Doc](https://docs.google.com/spreadsheets/d/1rCPcZ34nXKxOnmgu5WRZBONL1y4N0vczJLod0S6F4zE/edit#gid=0)
<br /><br /><br />

|Type                | Value                  |Quantity|
|--------------------|:----------------------:|-------:|
|Codeword-Names      |Generic word            |7       |
|Codeword-Places     |Generic word            |13      |
|Codeword-Colors     |Generic word            |91      |
|Homophonic-(r)      |Random odd int (0-999)  |1       | 
|Homophonic-(l)      |Random odd int (0-999)  |1       | 
|Homophonic-('space')|Random odd int (0-999)  |1       |
|Null character      |Random int (-999) ~ (-1)|1       |

<br /><br />

## HOW IT WORKS
**ENCODING:**\
The input string will be split into individual words and spaces, which are then stored in a list in their original sequence. The program processes each element of this list in the following manner:

1. It first checks if the current word matches any pre-defined code words in the system.

2. If no match is found, the word is broken down into its constituent characters. Each character is then converted into its corresponding ASCII integer value.

3. The system performs a lookup to find a word that corresponds to this numerical value. This encoded word is then added to a new list.

4. If a character is a homophone, a special homophonic value (a random integer) is used instead.

5. The final step involves adding null characters to the list. These null characters are inserted at a rate of 30% of the total number of words, ensuring that no two null characters are placed consecutively.

This process results in a transformed list of encoded words and special values.
<br />

**DECODING:**\
Upon receiving the encoded string, the initial step involves splitting it by all spaces and storing the segments in a List. In the encoded string, spaces are redundant since they were already accounted for during encoding; thus, they are omitted when splitting. The next phase involves eliminating all Null characters from this sequence.

Once the null characters are removed, the decoding process begins. The procedure is as follows:

1. Each word in the List is examined. If a word corresponds to a pre-defined code word, this match is recorded.

2. For words not found in the code word repository, a search is conducted in the ASCII dictionary. Since each ASCII number is associated with a unique word, the encoded string, when processed using the same encoding algorithm, should yield a recognizable result.

After assembling each decoded word, the system ultimately outputs the original, decoded message.


## EXAMPLES
**TEXT:**<mark>hello world!</mark>\
**ENCODED:** <ins>check queen 782 358 promotion -76 88709 opening promotion 343 -222 -294 460 king nightingale -820</ins>\
***
**TEXT:** <mark>three r, five spaces, finally four l:rrr     llll</mark>\
**ENCODED:** <ins>draw check 207 queen queen 84277 -690 -384 -564 697 shadow -142 -54 74229 pawn checkmate endgame queen 72182 stalemate -118 zugzwang -816 yankee mirror queen stalemate shadow 79099 -358 pawn checkmate -96 sacrifice yankee 976 64 tactics 86678 pawn -276 promotion resignation 673 23132 626 kilo 911 -350 673 301 -742 -824 41499 -636 33988 29514 42421 81892 778 272 814 862 -266</ins>\

## ERROR HANDLING
The program can only encode character appears on the EN-Keyboard excluding Capital letters. Thus, characters from extended ASCII range, characters in Kanji, and other special characters will not be processed. Below is the example of error handling:\
**TEXT:**  <mark>中文汉字</mark>\
**RESULT** <ins>{"operation":"Encoder","author":"Team A","encodedCiphertext":"error, character outside the ASCII range, input should be regular letters on the keyboard!"}</ins>

## ARCHITECTURE & CODE
This project operates within a .NetCore (8.0) framework and is executed in a Docker environment hosted on an AWS EC2 instance. Code updates are managed by pulling new versions from a GitHub repository, facilitating version upgrades.\
GitHub Link: