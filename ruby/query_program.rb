t1 = Time.now
require_relative('./querier.rb')
querier = Querier.new
query = ARGV.first

if query == "a"
  nebraskans = querier.get_all_from_state "Nebraska"
  puts "Number of users from Nebraska: #{nebraskans.size}"
elsif query == "b"
  num_users_messaging_at_8 = querier.get_num_users_messaging_during(8, 9)
  puts "Number of users messaging from 8am to 9am inclusive: #{num_users_messaging_at_8}"
elsif query == "c"
  num_nebraskans_messaging_at_8 = querier.get_users_from_state_messaging_during("Nebraska", 8, 9).size
  puts "Number of Nebraskans messaging from 8am to 9am inclusive: #{num_nebraskans_messaging_at_8}"
else
  max_nebraskan_at_8 = querier.get_user_max_messages_during("Nebraska", 8, 9)
  puts "ID of Nebraskan sending most messages from 8am to 9am: #{max_nebraskan_at_8.id}"
end

t2 = Time.now
delta = t2 - t1
puts "Query took #{delta} seconds"