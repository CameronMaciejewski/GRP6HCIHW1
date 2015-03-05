#GRP6HCIHW1
Group 6's implementation of the coding portion of CSCE378H homework assignment 1

--
Ruby (Used for initial, non B+ tree stages of project)

The initial stages of this project were completed using Ruby programs.

Run 'ruby/transform_records_program.rb' to transform the initial data files into files matching our models for this data.

Run 'ruby/sort_records_program.rb' to sort the rows generated by the above program. The first argument to this program should be the "table" to sort (either 'user' or 'message'). The second argument should be the 'row' (field) by which to sort (e.g. 'name' or 'day'). A sample command would look like this: "ruby ruby/sort_records_program.rb user city", which would sort all users by lexigraphical order of city.

Our basic, non B+ tree queries (step 5) were also completed with ruby programs. 

Run 'ruby/query_program.rb' to perform each query, passing the letter corresponding to the query as an argument. For example, to run query (c), you would enter "ruby ruby/query_program.rb c".

A brief summary of each query follows:
-a) Prints the total number of users that are from the state of Nebraska
-b) Prints the total number of users who sent messages from 8am to 9am (inclusive)
-c) Prints the total number of users from Nebraska who sent messages from 8am to 9am.
-d) Prints the user from Nebraska who has sent the most messages from 8am to 9am.