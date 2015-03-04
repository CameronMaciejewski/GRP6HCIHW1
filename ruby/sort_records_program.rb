require_relative('./record_sorter.rb')

t1 = Time.now
record_sorter = RecordSorter.new
record_sorter.sort(ARGV[0], ARGV[1].to_sym)
t2 = Time.now
delta = t2 - t1
puts "sort took #{delta} seconds"